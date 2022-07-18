using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TabletManagerWPF.View
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private string NewPassword { get; set; }
        private string ConfirmPassword { get; set; }
        public Operator Op { get; }

        public ChangePassword(Operator op)
        {
            InitializeComponent();
            Op = op;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            NewPassword = NewPasswordBox.Password;
        }

        private void PasswordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            ConfirmPassword = ConfirmPasswordBox.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NewPassword==ConfirmPassword && NewPassword!=null)
            {
                CommonDatabase.CommonDbAccess.SetOperatorPassword(Op.ACPno, NewPassword);
                MessageBox.Show("Hasło zostało poprawnie ustawione");
                this.Close();
            }
            else
            {
                MessageBox.Show("Hasło nie zostało poprawnie ustawione");
                this.Close();
            }
        }
    }
}
