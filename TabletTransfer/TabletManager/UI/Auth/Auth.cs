using CommonDatabase.Data;
using CommonDatabase.Libs;
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

namespace TabletManager
{
    public partial class Auth : Form
    {
        Subject<Operator> onAuthenticated = new Subject<Operator>();
        public IObservable<Operator> OnAuthenticated => onAuthenticated.AsObservable();
        
        Subject<bool> onCancel = new Subject<bool>();
        public IObservable<bool> OnCancel => onCancel.AsObservable();

        public Auth()
        {
            InitializeComponent();
            
            label1.Text = $"TabletManager v.{Application.ProductVersion}";

            comboBox1.Items.AddRange(CommonDatabase.CommonDbAccess.GetAllLocations());
            comboBox1.SelectedIndex = 0;//TODO: add option to load from user settings last user location

            if (!DesignMode)
            {
                StateSingleton.Instance.BarcodeHandlerMessageReceived
                        .Where(x => this.Visible)
                        .Where(x => StateSingleton.Instance.BarcodeIsSim || Form.ActiveForm == this)//breakpoint before this point won't be a good idea.
                        .Select(x => BarcodeOperations.BadgeDecoder(x.Content))
                        .Where(x => x != null)
                        .ObserveOn(this)
                        .Subscribe((x) => {
                            textBox1.Text = x.ACPNo;
                            textBox2.Focus();
                        });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            onCancel.OnNext(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var op = CommonDatabase.CommonDbAccess.GetOperator(textBox1.Text, comboBox1.Text, textBox2.Text);
            if (op != null)
            {
                switch (op.PasswordVerified)
                {
                    case Operator.PasswordVerificationResult.NoPasswordSet:
                        MessageBox.Show("Pierwsze logowanie. Proszę ustawić hasło.");
                        
                        bPassChange_Click(null, null);
                        
                        break;
                    case Operator.PasswordVerificationResult.OK:
                        if (op.IsAllowed(Operator.PositionEnum.SL))
                        {
                            StateSingleton.Instance.SetAuthenticatedUser(op);
                            StateSingleton.Instance.Location = comboBox1.Text;

                            onAuthenticated.OnNext(op);
                        }
                        else
                        {
                            MessageBox.Show("Nie masz uprawnień. Logowanie zabronione.");
                        }
                        break;
                    case Operator.PasswordVerificationResult.WrongPassword:
                        MessageBox.Show("Błędne hasło. Logowanie zabronione.");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Brak użytkownika w systemie. Logowanie zabronione.");
            }
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void bPassChange_Click(object sender, EventArgs e)
        {
            AuthPassChange apc = new AuthPassChange();
            
            apc.AcpNo = textBox1.Text;
            apc.OldPass = textBox2.Text;

            apc.ShowDialog();
        }
    }
}
