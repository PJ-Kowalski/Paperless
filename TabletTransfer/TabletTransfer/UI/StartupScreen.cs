using CommonDatabase.Tools;
using HidBarcodeHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletTransfer.AI;
using TabletTransfer.UI.Controls;

namespace TabletTransfer.UI
{
    public partial class StartupScreen : MyScreen
    {
        
        public StartupScreen()
        {
            InitializeComponent();
            bClear.Visible = false;
            bOk.Enabled = false;
            operatorBox1.Clear();
        }

        protected override void BarcodeHandler_MessageReceived(object source, HidBarcodeHandler.HidBarcodeHandler.MessageReceivedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { BarcodeHandler_MessageReceived(source, e); }));
                return;
            }
            LoadingWindow.LoadingIndicator loading = new LoadingWindow.LoadingIndicator();
            loading.Show();
            
            Debug.WriteLine(e.Content);
            textBox1.Text = e.Content.ToString();
           
            try
            {
                if (StateSingleton.Instance.DoScan(BarcodeOperations.BadgeDecoder(e.Content).ToString()))
                {
                    loading.Close();
                    operatorBox1.Set(StateSingleton.Instance.SL);
                    Invoke(new Action(() =>
                    {
                        bClear.Visible = true;
                        bOk.Enabled = true;
                    }));
                }
                else
                {
                    loading.Close();
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
            if (tbReason.Text.Trim() == "")
            {
                MessageBox.Show("Wprowadź powód wyłączenia.");
                return;
            }
            if (StateSingleton.Instance.FinishVerifyLastShutdown(tbReason.Text))
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
