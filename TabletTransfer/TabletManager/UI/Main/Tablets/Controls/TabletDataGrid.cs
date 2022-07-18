using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletManager.AI;

namespace TabletManager.UI.Controls
{
    public partial class TabletDataGrid : UserControl
    {
        Subject<IEnumerable<Tablet>> selectionChanged = new Subject<IEnumerable<Tablet>>();
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public IObservable<IEnumerable<Tablet>> SelectionChanged => selectionChanged.AsObservable();

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool AllowToChangeOnlyMine { get => cbFilterMine.Enabled; set => cbFilterMine.Enabled = value; }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowOnlyMine { get => cbFilterMine.Checked; set => cbFilterMine.Checked = value; }

        private string location;
        public void RefreshData(string location = null)
        {
            //save row position after refresh
            int saveRow = 0;
            if (dataGridView1.Rows.Count > 0 && dataGridView1.FirstDisplayedCell != null)
                try
                {
                    saveRow = dataGridView1.SelectedRows[0].Index;
                }
                catch (Exception)
                {

                    saveRow =0;
                }

            if (DesignMode) return;

            this.location = location;
            dataGridView1.Rows.Clear();

            Operator user = cbFilterMine.Checked ? user = StateSingleton.Instance.AuthenticatedUser.Value : null;

            foreach (var t in CommonDatabase.CommonDbAccess.GetTablets(location, user))
            {
                dataGridView1.Rows.Add(t.GetDefaultRow());
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = t;
            }
            dataGridView1.SelectedRows.Cast<DataGridViewRow>().ToList().ForEach(x => x.Selected = false);

            //recover row position after refresh
            if (saveRow < dataGridView1.Rows.Count)
                dataGridView1.Rows[saveRow].Selected = true;// = saveRow;

        }
        public int TabletsCount { get => dataGridView1.Rows.Count; }
        public TabletDataGrid()
        {
            InitializeComponent();

            Observable
                .FromEventPattern(h => cbFilterMine.CheckedChanged += h, h => cbFilterMine.CheckedChanged -= h)
                .Subscribe((x) => {
                    RefreshData(location);
                });

            Observable
                .FromEventPattern(h => dataGridView1.SelectionChanged += h, h => dataGridView1.SelectionChanged -= h)
                .Subscribe((x) => {
                    selectionChanged.OnNext(SelectedTablets);
                });

            StateSingleton.Instance.AuthenticatedUser
                .AsObservable()
                .Subscribe((x) => {
                    RefreshData(StateSingleton.Instance.Location); 
                });
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<Tablet> SelectedTablets
        {
            get
            {
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    if (r.Tag is Tablet)
                    {
                        yield return r.Tag as Tablet;
                    }
                }
            } 
        }
    }
}
