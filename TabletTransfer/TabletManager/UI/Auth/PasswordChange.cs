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
    public partial class AuthPassChange : Form
    {
        public string AcpNo { get { return tbAcpNo.Text; } set { tbAcpNo.Text = value; } }
        public string OldPass { get { return tbOldPass.Text; } set { tbOldPass.Text = value; } }
        public AuthPassChange()
        {
            InitializeComponent();
            
            label1.Text = $"TabletManager v.{Application.ProductVersion}";

            if (!DesignMode)
            {
                StateSingleton.Instance.BarcodeHandlerMessageReceived
                        .Where(x => this.Visible)
                        .Where(x => StateSingleton.Instance.BarcodeIsSim || Form.ActiveForm == this)
                        .Select(x => BarcodeOperations.BadgeDecoder(x.Content))
                        .Where(x => x != null)
                        .ObserveOn(this)
                        .Subscribe((x) => {
                            tbAcpNo.Text = x.ACPNo;
                            tbOldPass.Focus();
                        });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbNewPass1.Text != tbNewPass2.Text)
            {
                MessageBox.Show("Nie udało ci się powtórnie wpisać tego samego nowego hasła.");
                return;
            }
            if (tbNewPass1.Text == "1234")
            {
                MessageBox.Show("Hasło 1234 jest zabronione.");
                return;
            }
            var op = CommonDatabase.CommonDbAccess.GetOperator(tbAcpNo.Text, null, tbOldPass.Text);
            if (op.PasswordVerified == Operator.PasswordVerificationResult.WrongPassword)
            {
                MessageBox.Show("Błędnie wpisałeś stare hasło. Nie można kontynuować.");
                return;
            }
            
            if (CommonDatabase.CommonDbAccess.SetOperatorPassword(tbAcpNo.Text, tbNewPass1.Text))
            {
                MessageBox.Show("Hasło poprawnie zmienione");
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void tbAcpNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbOldPass.Focus();
            }
        }

        private void tbOldPass_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbNewPass1.Focus();
            }
        }

        private void tbNewPass1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbNewPass2.Focus();
            }
        }

        private void tbNewPass2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
