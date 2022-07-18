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

namespace TabletManagerWPF.View.Pages
{
    /// <summary>
    /// Interaction logic for AddComputer.xaml
    /// </summary>
    public partial class AddComputer : Window
    {
        public AddComputer(in Operator op, in MainWindowVM vm)
        {
            this.DataContext = new AddComputerVM(op, vm);
            InitializeComponent();
        }
    }
}
