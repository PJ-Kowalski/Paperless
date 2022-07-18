using CommonDatabase.Tools;
using HidBarcodeHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletTransfer.AI;

namespace TabletTransfer.UI
{
    public partial class AddTabletScreen : MyScreen
    {
        public AddTabletScreen()
        {
            InitializeComponent();

            bClear.Visible = false;
            bOk.Enabled = false;
            operatorBox1.Clear();

            comboBox1.Items.AddRange(StateSingleton.Instance.GetAllLocations());
        }
        protected override void BarcodeHandler_MessageReceived(object source, HidBarcodeHandler.HidBarcodeHandler.MessageReceivedEventArgs e)
        {
            Debug.WriteLine(e.Content);
            try
            {
                if (StateSingleton.Instance.DoScan(BarcodeOperations.BadgeDecoder(e.Content).ToString()))
                {
                    operatorBox1.Set(StateSingleton.Instance.SL);
                    if (StateSingleton.Instance.SL.CanWork(null, "Próba dodania urządzenia przez osobę nieobecną"));
                    {
                        Invoke(new Action(() =>
                        {
                            bClear.Visible = true;
                            bOk.Enabled = true;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            operatorBox1.Clear();
            bClear.Visible = false;
            bOk.Enabled = false;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (StateSingleton.Instance.FinishAdd(comboBox1.Text))
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
