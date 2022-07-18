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
using TabletTransfer.AI.Tools;
using Oracle.ManagedDataAccess.Client;
using TabletTransfer.UI.Controls;

namespace TabletTransfer.UI
{
    public partial class LockedScreen : MyScreen
    {
        
        public LockedScreen()
        {
            InitializeComponent();
            bClear_Click(null, null);
            versionLabel.Text = Application.ProductVersion;
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
            bool unlocked = false;
            textBox1.Text = e.Content;
            if (true)// BarcodeDataEvaluator.EvaluateBarcodeData(e.Content)
            {
                if (StateSingleton.Instance.DoScan(BarcodeOperations.BadgeDecoder(e.Content).ToString()))
                {
                    textBox1.Text = e.Content;
                    operatorBox1.Set(StateSingleton.Instance.SL);
                    loading.Close();
                    if (StateSingleton.Instance.Myself.LockType.AnySlCanUnlock || StateSingleton.Instance.Myself.LockedBy == StateSingleton.Instance.SL || StateSingleton.Instance.SL.Position== Operator.PositionEnum.Service)
                    {
                        unlocked = true;
                    }
                }
                loading.Close();
                Invoke(new Action(() =>
                {
                    if (unlocked)
                    {
                        bClear.Visible = true;
                        bOk.Enabled = true;
                        lMsg.Text = "Możesz odblokować.";
                        StateSingleton.Instance.NextUser = null;
                    }
                    else
                    {
                        if (StateSingleton.Instance.Myself.LockType.AnySlCanUnlock)
                        {
                            lMsg.Text = "Tylko SL może odblokować tablet.";
                        }
                        else
                        {
                            lMsg.Text = "Tylko serwis może go odblokwać.";
                        }
                    }
                }));
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            operatorBox1.Clear();
            bClear.Visible = false;
            bOk.Enabled = false;
            
            lMsg.Text = "";
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            bClear_Click(null, null);

            if (_lockType.Name =="DAMAGE")
            {
                StateSingleton.Instance.CurrentUser = StateSingleton.Instance.SL;
                StateSingleton.Instance.StartVerifyLastShutdown();
            }
            else
            {
                StateSingleton.Instance.CurrentUser = StateSingleton.Instance.SL;
                StateSingleton.Instance.DoPass(true);
            }
        }

        LockType _lockType;
        internal void Setup(LockType lockType, string who, string why)
        {
            lDescription.Text = lockType.Description;
            lLockdownComment.Text = why;
            lLockdownType.Text = lockType.Name;
            lWho.Text = who;
            _lockType = lockType;
        }
    }
}
