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
using CommonDatabase.Tools;
using TabletTransfer.AI.Tools;
using TabletTransfer.UI.Controls;

namespace TabletTransfer.UI
{
    public partial class PassDeviceScreen : MyScreen
    {
        //Loading loading = new Loading();
        public PassDeviceScreen()
        {
            InitializeComponent();

            Clear();
        }

        private void Clear()
        {
            operatorBox1.Clear();
            operatorBox2.Clear();

            tlpConfirm.Visible = false;
            lPrevScanConfirm.Visible = false;
        }
        
        protected override void BarcodeHandler_MessageReceived(object source, HidBarcodeHandler.HidBarcodeHandler.MessageReceivedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { BarcodeHandler_MessageReceived(source, e); }));
                return;
            }
            //LoadingWindow.LoadingIndicator loading = new LoadingWindow.LoadingIndicator();
            //loading.Show();
            Debug.WriteLine(e.Content);
            textBox1.Text = e.Content; //pk visualize barcode reader data
                                       //pk check if darcode data is ACP....

            if (true)//BarcodeDataEvaluator.EvaluateBarcodeData(e.Content)
            {
                if (StateSingleton.Instance.CurrentUser == null)
                {
                    StateSingleton.Instance.AuthenticateAsCurrent(BarcodeOperations.BadgeDecoder(e.Content).ToString());
                    textBox1.Text = "evaluation done";
                    //loading.Close();
                    if (StateSingleton.Instance.CurrentUser != null) //null is when user not exist in database
                    {
                        operatorBox1.Set(StateSingleton.Instance.CurrentUser);
                        if (StateSingleton.Instance.CurrentUser.CanWork(StateSingleton.Instance.Myself, "Próba przekazania przez osobę nieobecną."))
                        {
                            lPrevScanConfirm.Visible = true;
                            lInstruction.Text = "Zeskanuj nowego operatora";
                        }
                        else
                        {
                            lPrevScanConfirm.Visible = false;
                            lInstruction.Text = "wyjątek bezpieczeństwa";
                            StateSingleton.Instance.AddAlert("ALERT_2", "Próba przekazania osobie nieobecnej");
                            
                        } 
                    }
                    else
                    {
                        textBox1.Text = "Użytkownik nie istnieje w bazie danych";
                    }
            }
                else if (StateSingleton.Instance.NextUser == null)
                {
                    //loading.Close();
                    var tmp = BarcodeOperations.BadgeDecoder(e.Content).ToString();
                    if (tmp != StateSingleton.Instance.CurrentUser.ACPno || StateSingleton.Instance.CurrentUser.Position.ToString() == "Service" || StateSingleton.Instance.CurrentUser.Position.ToString() == "TL" || StateSingleton.Instance.CurrentUser.Position.ToString()== "SL")  //lokc prevent to tas to himself
                    {
                        StateSingleton.Instance.AuthenticateAsNext(tmp);

                        if (StateSingleton.Instance.NextUser != null) //null is when user not exist in database
                        {
                            //loading.Close();
                            operatorBox2.Set(StateSingleton.Instance.NextUser);

                            if (StateSingleton.Instance.NextUser.CanWork(StateSingleton.Instance.Myself, "Próba odebrania przez osobę nieobecną."))
                            {
                                if (StateSingleton.Instance.AuthenticateAsSL(tmp))
                                {
                                    DialogResult dialogResult = MessageBox.Show("Czy chesz przekazać tablet do magazynu?", "Blokada", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        StateSingleton.Instance.CurrentState = StateSingleton.StateEnum.InStorage;
                                        Clear();                                   
                                        StateSingleton.Instance.CurrentUser = StateSingleton.Instance.NextUser;
                                        StateSingleton.Instance.SL = StateSingleton.Instance.CurrentUser;
                                        passDevice(true);
                                        StateSingleton.Instance.PutInStorage();
                                        useSL = false;
                                        this.Hide();
                                        return;
                                    }
                                }
                                tlpConfirm.Visible = true;

                                lInstruction.Text = "Nowy operator potwierdza stan urządzenia";
                            }
                            else
                            {
                                //loading.Close();
                                tlpConfirm.Visible = false;
                                StateSingleton.Instance.AddAlert("ALERT_2", "wyjątek Bezpieczeństwa");
                                lInstruction.Text = "Wyjątek bezpieczeństwa";
                            } 
                        }
                        else
                        {
                            //loading.Close();
                            textBox1.Text = "Użytkownik nie istnieje w bazie danych";
                        }
                    }
                }
            }
            else
            {
                //loading.Close();
                textBox1.Text = "Błąd odczytu kodu";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            operatorBox1.Clear();
            StateSingleton.Instance.CurrentUser = null;
            StateSingleton.Instance.NextUser = null;
            StateSingleton.Instance.SL = null;
            if (useSL)
            {
                StateSingleton.Instance.DoLockdown();
            }
            useSL = false;

            this.Close();
            Clear();
        }
        private void bTabletOk_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "UWAGA!\n\nWłaśnie potwierdzasz, że urządzenie które otrzymałeś jest w dobrym stanie technicznym!\nOd teraz TY za nie odpowiadasz!\n\nCzy na pewno?",
                "UWAGA!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation 
            ) == DialogResult.Yes)
            {
                passDevice(true);
                GC.Collect();
                Clear();
            }
        }

        private void bTabletNG_Click(object sender, EventArgs e)
        {
            passDevice(false);
            Clear();
        }
        
        private void passDevice(bool tabletOk)
        {
            //cbCancel.Visible = false;
            StateSingleton.Instance.FinishPass(tabletOk);
            Close();
            /*Task.Delay(new TimeSpan(0, 0, 3)).ContinueWith(o => {
                Invoke(new Action(() => {
                    
                }));
            });*/
        }
        private bool useSL = false;
        internal void UseSl()
        {

            //StateSingleton.Instance.CurrentUser = StateSingleton.Instance.SL;  //pk rem becouse last user was not right
            operatorBox1.Set(StateSingleton.Instance.CurrentUser);
            if (StateSingleton.Instance.CurrentUser.CanWork(StateSingleton.Instance.Myself,"Próba przekazania przez osobę nieobecną."))
            {
                useSL = true;
                lPrevScanConfirm.Visible = true;

                lInstruction.Text = "Zeskanuj nowego operatora";
            }
        }
    }
}
