using CommonDatabase.Data;
using CommonDatabase.Tools;
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

namespace TabletManager.UI
{
    public partial class AddOperator : UserControl
    {
        public AddOperator()
        {
            InitializeComponent();

            operatorBox1.Clear();
            operatorBox1.SetSize(10);
        }
        Subject<(string acpno, Operator.PositionEnum position)> newOne = new Subject<(string acpno, Operator.PositionEnum position)>();
        public IObservable<(string acpno, Operator.PositionEnum position)> NewOne => newOne.AsQbservable();

        private void bOk_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                Hide();
            }
            var op = operatorBox1.Get();
            newOne.OnNext((op.ACPno, (Operator.PositionEnum)cbPosition.SelectedItem));//TODO: add cbPosition, move postion to location
            operatorBox1.Clear();
        }

        private void tbAcpno_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bSearch_Click(null, null);
            }
            bSearch.Enabled = (tbAcpno.Text != "");
        }
        private void tbAcpno_TextChanged(object sender, EventArgs e)
        {
            bSearch.Enabled = (tbAcpno.Text != "");
        }
        private void bSearch_Click(object sender, EventArgs e)
        {
            var someone = CommonDatabase.CommonDbAccess.GetSomeone(tbAcpno.Text);
            operatorBox1.Set(someone);
            bOk.Enabled = someone != null;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            operatorBox1.Clear();
            Hide();
        }

        private void AddOperator_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                StateSingleton.Instance.BarcodeHandlerMessageReceived
                    .Where(x => this.Visible)
                    .Where(x => StateSingleton.Instance.BarcodeIsSim || Form.ActiveForm == this.Parent)
                    .Select(x => BarcodeOperations.BadgeDecoder(x.Content))
                    .Where(x => x != null)
                    .ObserveOn(this)
                    .Subscribe((x) =>
                    {
                        tbAcpno.Text = x.ACPNo;
                        bSearch_Click(null, null);
                    });
                CommonDatabase.CommonDbAccess.GetPositions()
                    .Where(x => x > StateSingleton.Instance.AuthenticatedUser.Value.Position)//my position
                    .ToList()
                    .ForEach(x =>
                    {
                        cbPosition.Items.Add(x);//addrange won't work on enum[]
                });
            }
        }
    }
}
