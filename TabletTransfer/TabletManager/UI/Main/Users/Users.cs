using CommonDatabase.Data;
using CommonDatabase.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletManager.AI;
using TabletManager.UI.CommonControls;

namespace TabletManager.UI.Controls
{
    public partial class Users : UserControl, ILocationAwareControl
    {
        List<CheckBox> filters = new List<CheckBox>();
        public string TabletLocation { get; set; }
        public Users()
        {
            InitializeComponent();
            
            cbBlue.Tag = Operator.PositionEnum.Normal;
            cbSL.Tag = Operator.PositionEnum.SL;
            cbTL.Tag = Operator.PositionEnum.TL;
            cbService.Tag = Operator.PositionEnum.Service;

            filters.AddRange(new CheckBox[] { cbBlue, cbSL, cbTL, cbService });

            Observable
                .FromEventPattern(h => Load += h, h => Load -= h)
                .Select(op => StateSingleton.Instance.AuthenticatedUser.Value)
                .Concat(StateSingleton.Instance.AuthenticatedUser)
                .Where(x => !DesignMode)
                .Subscribe(op => {
                    RefreshDataUsingFilters(op);
                    ReloadDataBasedOnAuthenticatedUser(op);
                });

            addOperator1.NewOne
                .ObserveOn(this)
                .Subscribe(x =>
                {
                    var alreadyExists = dataGridView1.Rows.Cast<DataGridViewRow>()
                        .Select(a => new { op = a.Tag as Operator, row = a })
                        .Any(a => a.op.ACPno == x.acpno);

                    if (alreadyExists)
                    {
                        MessageBox.Show("Ten użytkownik ma już przyznane uprawnienia w tej lokalizacji. W celu zmiany, usuń go z listy i dodaj ponownie z dowolnymi uprawnieniami.");
                    }
                    else
                    {
                        if (CommonDatabase.CommonDbAccess.AddOperatorToLocation(TabletLocation, x.acpno, x.position))
                        {
                            Operator new_op = CommonDatabase.CommonDbAccess.GetOperator(x.acpno, TabletLocation);

                            dataGridView1.Rows.Add(new_op.GetDefaultRow());
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = new_op;
                        }
                    }
                });
        }
        private void ReloadDataBasedOnAuthenticatedUser(Operator op)
        {
            filters.ForEach(filter =>
            {
                filter.Checked = filter.Enabled = false;
                filter.CheckedChanged += Filter_CheckedChanged;
                filter.Enabled = filter.Checked = op.IsAllowed((Operator.PositionEnum)filter.Tag - 1);
            });

            addOperator1.Visible = false;
        }
        private void Users_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                //ReloadDataBasedOnAuthenticatedUser(StateSingleton.Instance.AuthenticatedUser);
            }
        }
        private void RefreshDataUsingFilters(Operator op)
        {
            dataGridView1.Rows.Clear();

            if (!String.IsNullOrEmpty(TabletLocation))
            {
                var underlings = CommonDatabase.CommonDbAccess.GetOperators(TabletLocation, op.GetUnderlings());

                underlings
                    .Where(x => filters.Any(f => f.Checked && x.Position == ((Operator.PositionEnum)f.Tag)))
                    .ToList()
                    .ForEach(x =>
                    {
                        dataGridView1.Rows.Add(x.GetDefaultRow());
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = x;
                    });
            }
        }
        private void Filter_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDataUsingFilters(StateSingleton.Instance.AuthenticatedUser.Value);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bRemove.Enabled = dataGridView1.SelectedRows.Count > 0;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            addOperator1.Visible = true;
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var persons = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                    .Select(x => new { op = x.Tag as Operator, row = x })
                    .Where(x => x.op.ACPno != StateSingleton.Instance.AuthenticatedUser.Value.ACPno)
                    .ToList();

                var part = persons.Count() < 10 ? String.Join("\n", persons.Select(x => x.op)) : $"[dużo osób! {persons.Count()}]";
                if (MessageBox.Show($"Następujące osoby zostaną usunięte z listy: \n{part}.\n\nCzy na pewno chcesz kontynuować?","UWAGA!", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    persons.ForEach(x =>
                    {
                        if (CommonDatabase.CommonDbAccess.CheckIfOperatorIsResponsibleForTablets(x.op) == null)
                        {
                        CommonDatabase.CommonDbAccess.RemoveOperatorFromLocation(TabletLocation, x.op);
                        dataGridView1.Rows.Remove(x.row);
                        }
                        else
                        {
                            string tab = "";
                            foreach (var item in CommonDatabase.CommonDbAccess.CheckIfOperatorIsResponsibleForTablets(x.op))
                            {
                                tab = tab + " " + item.ToString();
                            }
                            MessageBox.Show("Użytkownik ma dalej przypisane tablety, przekaż wszystkie tablety przed usynięciem uzytkownika" + tab);
                            
                        }
                    });
                }
            }
        }

        void ILocationAwareControl.SetTabletLocation(string location)
        {
            TabletLocation = location;

            RefreshDataUsingFilters(StateSingleton.Instance.AuthenticatedUser.Value);
        }

        private void bBadge_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var persons = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                    .Select(x => new { op = x.Tag as Operator, row = x })
                    .ToList();

                persons.ForEach(x =>
                {
                    var now = DateTime.Now;
                    var expiration_date = CommonDatabase.CommonDbAccess.AddTemporaryBadge(x.op, now, StateSingleton.Instance.AuthenticatedUser.Value);
                    if (expiration_date.HasValue)
                    {
                        Printer p = new Printer();
                        string template = File.ReadAllText(Path.Combine("Resources", "badge.html"));
                        Dictionary<string, string> toReplace = new Dictionary<string, string>();

                        toReplace.Add("[NAME]", x.op.ToString());
                        toReplace.Add("[ACPNO]", x.op.ACPno.ToString());
                        toReplace.Add("[EXPIRATION]", expiration_date.Value.ToString("yyyy-MM-dd HH:mm"));
                        toReplace.Add("[BARCODE]", BarcodeOperations.GenerateOperatorBarcodeBase64(x.op, now));

                        p.SelectFile(template, toReplace);
                        p.ShowDialog();
                    }
                });
            }
            else
            {
                MessageBox.Show("Zaznacz jednego operatora.");
            }
        }
    }
}
