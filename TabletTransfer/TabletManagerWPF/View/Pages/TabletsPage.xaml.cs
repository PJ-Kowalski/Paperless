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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TabletManagerWPF.ViewModel;

namespace TabletManagerWPF.View.Pages
{
    /// <summary>
    /// Interaction logic for TabletsPage.xaml
    /// </summary>
    public partial class TabletsPage : Page
    {
        public TabletsPage(in Operator op, in MainWindowVM vm)
        {
            this.DataContext = new TabletPageVM(op, vm);
            InitializeComponent();
        }

        //private List<TabletEvent> _events;
        //public List<TabletEvent> Events
        //{
        //    get { return _events; }
        //    set { _events = value; }
        //}

        //private List<Alert> _alerts;
        //public List<Alert> Alerts
        //{
        //    get { return _alerts; }
        //    set { _alerts = value; }
        //}


    }
}
