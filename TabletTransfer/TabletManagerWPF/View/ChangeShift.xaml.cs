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
using TabletManagerWPF.ViewModel;

namespace TabletManagerWPF.View
{
    /// <summary>
    /// Interaction logic for ChangeShift.xaml
    /// </summary>
    public partial class ChangeShift : Window
    {
        public delegate void PasswordChanged(string password);
        public event PasswordChanged OnPasswordChanged;
        public ChangeShift(in Operator op)
        {
            this.DataContext = new ChangeShiftVM(op, this);
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (OnPasswordChanged!=null)
            {
                OnPasswordChanged(PasswordBox.Password);
            }

        }
    }
}
