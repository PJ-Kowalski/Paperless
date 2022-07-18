using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletTransfer.AI;
using HidBarcodeHandler;
using CommonDatabase.Data;
using CommonDatabase.Tools;

namespace TabletTransfer.UI
{
    public partial class AnnotateScreen : MyScreen
    {
        public AnnotateScreen()
        {
            InitializeComponent();
            
            operatorBox1.Clear();
            operatorBox2.Clear();

            
            lPrevScanConfirm.Visible = false;
            //lNextScanConfirm.Visible = false;

            //BarcodeHandlerMessageReceived = BarcodeHandler_MessageReceived;
        }
        bool now_sl_confirms = false;
        protected override void BarcodeHandler_MessageReceived(object source, HidBarcodeHandler.HidBarcodeHandler.MessageReceivedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { BarcodeHandler_MessageReceived(source, e); }));
                return;
            }
            Debug.WriteLine(e.Content);

            var tmp = BarcodeOperations.BadgeDecoder(e.Content).ToString();

            var someone = StateSingleton.Instance.Authenticate(tmp);

            if (someone.ACPno == StateSingleton.Instance.NextUser.ACPno && someone.CanWork(StateSingleton.Instance.Myself,"Próba odebrania urządzenia przez osobę nieobecną.") && !now_sl_confirms)
            {
                if (tbReasonOperator.Text == "")
                {
                    MessageBox.Show("Proszę najpierw opisz problem.");
                    return;
                }
                lPrevScanConfirm.Visible = true;
                bCancel.Enabled = false;
                tlpSLOptions.Visible = true;
                bOk.Enabled = false;
                tbReasonOperator.Enabled = false;
                operatorBox2.Set(StateSingleton.Instance.NextUser);
                now_sl_confirms = true;
            }
            else if (someone.IsAllowed(Operator.PositionEnum.SL))
            {
                StateSingleton.Instance.AuthenticateAsSL(tmp);

                operatorBox2.Set(StateSingleton.Instance.SL);
                if (StateSingleton.Instance.SL.CanWork(StateSingleton.Instance.Myself, "Próba potwierdzenia adnotacji przez osobę nieobecną."))
                {
                    now_sl_confirms = true;
                    bOk.Enabled = true;
                }
                else
                {
                    bOk.Enabled = false;
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
            operatorBox1.Clear();
            StateSingleton.Instance.CurrentUser = null;
            StateSingleton.Instance.NextUser = null;
            StateSingleton.Instance.SL = null;

            this.Hide();
        }

        internal void UseNext()
        {
            operatorBox1.Set(StateSingleton.Instance.NextUser);
            if (StateSingleton.Instance.NextUser.CanWork(StateSingleton.Instance.Myself, "Próba odebrania z adnotacją przez osobę nieobecną."))
            {
                StateSingleton.Instance.SL = null;
                lPrevScanConfirm.Visible = false;

                tbReasonOperator.Text = "";
                tbReasonOperator.Enabled = true;
                tlpSLOptions.Visible = false;
                bOk.Enabled = false;
                now_sl_confirms = false;
            }
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (tbReasonOperator.Text == "")
            {
                MessageBox.Show("Operator musi wykonać opis zgłoszonego uszkodzenia.");
                return;
            }
            Hide();
            if (radioButton1.Checked)
            {
                StateSingleton.Instance.FinishAnnotate(true, tbReasonOperator.Text, tbSlComment.Text);
            }
            else
            {
                if (radioButton2.Checked)
                {
                    StateSingleton.Instance.FinishAnnotate(false, tbReasonOperator.Text, tbSlComment.Text);
                }
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Hide();
            StateSingleton.Instance.CancelPass();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (StateSingleton.Instance.SL != null && StateSingleton.Instance.SL.CanWork(StateSingleton.Instance.Myself, "Próba odebrania z adnotacją przez osobę nieobecną."))
            {
                bOk.Enabled = true;
            }
            else
            {
                bOk.Enabled = false;
            }
        }
    }
}
