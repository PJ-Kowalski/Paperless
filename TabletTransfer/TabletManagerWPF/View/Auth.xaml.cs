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
using TabletManagerWPF.ViewModel;

namespace TabletManagerWPF.View
{
    /// <summary>
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public delegate void PasswordChangeEvent(string password);
        public event PasswordChangeEvent PasswordChange;
        public Auth()
        {
            InitializeComponent();
            this.DataContext = new AuthVM(this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordChange != null)
            {
                PasswordChange(PasswordBox.Password);
            }
        }
    }
}
