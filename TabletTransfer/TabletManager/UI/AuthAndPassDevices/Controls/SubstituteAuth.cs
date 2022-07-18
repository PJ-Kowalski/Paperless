using CommonDatabase.Data;
using CommonDatabase.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TabletManager.AI;

namespace TabletManager.UI.Controls
{
    public partial class SubstituteAuth : UserControl
    {
        public SubstituteAuth()
        {
            InitializeComponent();
            
            Observable
                .FromEventPattern(h => bClear.Click += h, h => bClear.Click -= h)
                .StartWith(new System.Reactive.EventPattern<object>(null, null))
                .Subscribe((x) => {
                    Clear();
                });

            StateSingleton.Instance.BarcodeHandlerMessageReceived
                .Where(x => this.Visible)
                .Where(x => StateSingleton.Instance.BarcodeIsSim || Form.ActiveForm == this.ParentForm)
                .Select(x => BarcodeOperations.BadgeDecoder(x.Content))
                .Where(x => x != null)
                .ObserveOn(this)
                .Subscribe((x) => {
                    textBox1.Text = x.ACPNo;
                    textBox1.Tag = "barcode";
                });

            Observable
                .FromEventPattern(h => textBox1.TextChanged += h, h => textBox1.TextChanged -= h)
                .Select(x => x.Sender as TextBox)
                .Do(x => {
                    if (x.Text.Length != "ACP173001".Length)
                    {
                        x.Tag = "keyboard";
                    }
                })
                .Where(x => x.Text.Length == "ACP173001".Length)
                .Subscribe((x) => {
                    var op = CommonDatabase.CommonDbAccess.GetOperator(textBox1.Text, StateSingleton.Instance.Location);
                    if (op != null)
                    {
                        operatorBox21.Set(op);
                        if (textBox1.Tag as string == "barcode")//if data comes from barcode reader, move cursor to password textbox
                        {
                            textBox2.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Użytkownik {x} nie ma odpowiednich uprawnień.");

                        Clear();
                    }
                });

            Observable
                .FromEventPattern(h => bOk.Click += h, h => bOk.Click -= h)
                .Concat(
                    Observable
                        .FromEventPattern<KeyEventHandler, KeyEventArgs>(h => textBox2.KeyDown += h, h => textBox2.KeyDown -= h)
                        .Where(x => x.EventArgs.KeyCode == Keys.Enter)
                        .Select(x => new EventPattern<object>(null, null))
                )
                .Subscribe((x) => {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show($"Wprowadź numer ACP pracownika. Możesz skorzystać z skanera.");
                        textBox1.Focus();
                        return;
                    }
                    if (textBox1.Text == StateSingleton.Instance.AuthenticatedUser.Value.ACPno && textBox1.Text != "ACP173001")
                    {
                        MessageBox.Show($"Odbiorca musi być inny niż osoba przekazująca.");
                        Clear();
                        return;
                    }
                    bool authOk = false;
                    var op = CommonDatabase.CommonDbAccess.GetOperator(textBox1.Text, StateSingleton.Instance.Location, textBox2.Text);
                    if (op != null && op.CanWork(null,"Próba przekazania urządzenia na osobę nieobecną (na PC)"))
                    {
                        switch (op.PasswordVerified)
                        {
                            case Operator.PasswordVerificationResult.NoPasswordSet:
                                AuthPassChange apc = new AuthPassChange();

                                apc.AcpNo = textBox1.Text;
                                apc.OldPass = "";

                                if (apc.ShowDialog() == DialogResult.OK)
                                {
                                    textBox2.Focus();
                                    authOk = true;
                                }
                                break;
                            case Operator.PasswordVerificationResult.OK:
                                authOk = true;
                                if (op.IsAllowed(Operator.PositionEnum.SL))
                                {
                                    operatorBox21.Set(op);

                                    AuthenticatedUser.OnNext(op);
                                }
                                else
                                {
                                    MessageBox.Show($"Autoryzacja nieudana. Użytkownik {textBox1.Text} nie ma odpowiednich uprawnień.");
                                }
                                break;
                            case Operator.PasswordVerificationResult.WrongPassword:
                                
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        authOk = false;
                    }
                    if (!authOk)
                    {
                        MessageBox.Show($"Autoryzacja nieudana. Użytkownik {textBox1.Text} nie ma odpowiednich uprawnień, nie istnieje lub podał błędne hasło.");

                        Clear();
                    }
                });
        }
        private void Clear()
        {
            AuthenticatedUser.OnNext(null);

            textBox1.Tag = null;
            textBox1.Clear();
            textBox2.Clear();
            operatorBox21.Clear();
            textBox1.Focus();
        }
        public BehaviorSubject<Operator> AuthenticatedUser = new BehaviorSubject<Operator>(null);
    }
}
