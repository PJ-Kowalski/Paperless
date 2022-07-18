using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TabletManagerWPF.Helpers;
using TabletManagerWPF.View;

namespace TabletManagerWPF.ViewModel
{
    public class AuthVM : BaseVM
    {
        public AuthVM(in Auth auth)
        {
            CommonDatabase.CommonDbAccess.Setup(Properties.Settings.Default.PaperlessConnectionString);
            Locations = CommonDatabase.CommonDbAccess.GetAllLocations().ToList();
            auth.PasswordChange += Auth_PasswordChange;
        }

        private void Auth_PasswordChange(string password)
        {
            Password=password;
        }

        #region Commands


        //command properity

        private ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand = new ExecuteCommand(() => Login(), true);


        #endregion

        public List<Location> Locations { get; set; }
        public string ACPno { get; set; }
        public string Password { get; set; }
        public string SelectedLocation { get; set; }



        public void Login()
        {
        if (SelectedLocation == String.Empty && ACPno!=null)
        {
            SelectedLocation = CommonDatabase.CommonDbAccess.GetACPLocation(ACPno);
        }
        var op = CommonDatabase.CommonDbAccess.GetOperator(ACPno, SelectedLocation, Password);
            if (op != null)
            {
                switch (op.PasswordVerified)
                {
                    case Operator.PasswordVerificationResult.NoPasswordSet:
                        MessageBox.Show("Pierwsze logowanie. Proszę ustawić hasło.");
                        ChangePassword pass = new ChangePassword(op);
                        pass.ShowDialog();
                        break;
                    case Operator.PasswordVerificationResult.OK:
                        if (op.IsAllowed(Operator.PositionEnum.SL))
                        {
                            Window auth = Application.Current.MainWindow;
                            MainWindow window = new MainWindow(op);
                            window.Show();
                            auth.Close();
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
    }

}
