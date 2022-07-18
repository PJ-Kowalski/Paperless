using CommonDatabase.Data;
using CommonDatabase.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TabletManagerWPF.Helpers;
using TabletManagerWPF.View;
using TabletManagerWPF.View.Pages;

namespace TabletManagerWPF.ViewModel
{
    public class TabletPageVM : BaseVM
    {

        public Operator op { get; }
        public MainWindowVM vm { get; }
        public TabletPageVM(in Operator Op,in MainWindowVM Vm)
        {
            addComputerVisible = false;
            Vm.OnLocationChanged += Vm_OnLocationChanged;
            op = Op;
            vm = Vm;
            OneDay = true;  
            TabletEvents = new List<TabletEvent>();
            Tablets = new List<Tablet>();
            Tablets = CommonDatabase.CommonDbAccess.GetTablets(vm.SelectedLocation);
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
            if (Op.Position == Operator.PositionEnum.TL || Op.Position == Operator.PositionEnum.Service)
            {
                addComputerVisible = true;
            }
        }


        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {


            Task t1 = Task<List<Tablet>>.Factory
                .StartNew(() =>
                 {
                    List<Tablet> tempTablets = new List<Tablet>(CommonDatabase.CommonDbAccess.GetTablets(vm.SelectedLocation));
                    return tempTablets;
                 }, TaskCreationOptions.LongRunning)
                .ContinueWith(tab =>
                 {
                    bool match = false;
                    foreach (var item in Tablets)
                    {
                        var l = tab.Result.Select(x => x)
                                          .Where(x => (item.WindowsName == x.WindowsName && item.LockType.Name != x.LockType.Name)
                                            || (item.WindowsName == x.WindowsName && item.Responsible.ACPno != x.Responsible.ACPno));
                        if (l.Count() > 0)
                        { match = true; break; }
                    }
                    if (match || Tablets.Count!=tab.Result.Count)
                    {
                        Tablets = tab.Result;
                    }
                    tab = null;                  
                 });
            GC.Collect();
        }

        private void Vm_OnLocationChanged(string location)
        {
            Tablets.Clear();
            Tablets = CommonDatabase.CommonDbAccess.GetTablets(location);
        }

        #region Commands

        private System.Windows.Input.ICommand _printTabletBarcodeCommand;
        public System.Windows.Input.ICommand PrintTabletBarcodeCommand => _printTabletBarcodeCommand = new ExecuteCommand(() => PrintTabletCode(), true);

        private System.Windows.Input.ICommand _changeLocationCommand;
        public System.Windows.Input.ICommand ChangeLocationCommand => _changeLocationCommand = new ExecuteCommand(() => ChangeTabletLocation(), true);

        private System.Windows.Input.ICommand _remoteLockCommand;
        public System.Windows.Input.ICommand RemoteLockCommand => _remoteLockCommand = new ExecuteCommand(() => RemoteLock(), true);

        private System.Windows.Input.ICommand _addComputerCommand;
        public System.Windows.Input.ICommand AddComputerCommand => _addComputerCommand = new ExecuteCommand(() => AddComputer(), true);



        #endregion


        #region Properites for view

        private Tablet _selectedTablet;
        public Tablet SelectedTablet
        {
            get { return _selectedTablet; }
            set { _selectedTablet = value;
                NotifyPropertyChanged(nameof(SelectedTablet));
                RefreshEventsForSelectedTablet();
            }
        }

        public bool addComputerVisible { get; set; }

        private bool _oneHour;
        public bool OneHour
        {
            get { return _oneHour; }
            set { _oneHour = value;
                if(value == true) {
                    SelectedTime = DateTime.Now.AddHours(-1);
                    RefreshEventsForSelectedTablet(); };
            }
        }

        private bool _tvelweHour;
        public bool TvelweHGour
        {
            get { return _tvelweHour; }
            set
            {
                _tvelweHour = value;
                if (value == true) {
                    SelectedTime = DateTime.Now.AddHours(-12);
                    RefreshEventsForSelectedTablet(); };
            }
        }
        private bool _oneDay;
        public bool OneDay
        {
            get { return _oneDay; }
            set { _oneDay = value;
                if (value == true) {
                    SelectedTime = DateTime.Now.AddDays(-1);
                    RefreshEventsForSelectedTablet(); };
            }
        }


        private List<TabletEvent> tabletEvents;
        public List<TabletEvent> TabletEvents
        {
            get { return tabletEvents; }
            set { tabletEvents = value;
                NotifyPropertyChanged(nameof(TabletEvents));
            }
        }

        private List<Tablet> _tablets;
        public List<Tablet> Tablets
        {
            get { return _tablets; }
            set { _tablets = value;
                NotifyPropertyChanged(nameof(Tablets));
            }
        }

        private bool _passOnly;
        public bool PassOnly
        {
            get { return _passOnly; }
            set { _passOnly = value;
                RefreshEventsForSelectedTablet();
            }
        }

        public DateTime SelectedTime { get; set; }


        #endregion

        private void RefreshEventsForSelectedTablet()
        {
            if (SelectedTablet != null&& Tablets!=null)
            {
                TabletEvents = CommonDatabase.CommonDbAccess.GetHistoryForTablets(Tablets, SelectedTime, DateTime.Now)
                    .Select(x=>x)
                    .Where(x=>x.SourceName == SelectedTablet.WindowsName)
                    .Where(x=>PassOnly? x.EventType== "TABLET_RECEIVED":true)
                    .ToList();
            }
            else if (Tablets != null)
            {
                TabletEvents = CommonDatabase.CommonDbAccess.GetHistoryForTablets(Tablets, SelectedTime, DateTime.Now)
                    .Where(x => PassOnly? x.EventType == "TABLET_RECEIVED" : true)
                    .ToList(); 
            }
        }

        private void PrintTabletCode()
        {
            if (SelectedTablet != null)
            {
                var now = DateTime.Now;
                string template = File.ReadAllText(Path.Combine("Resources", "tablet_barcode.html"))
                        .Replace("[HOSTNAME]", SelectedTablet.WindowsName)
                        .Replace("[BARCODE]", BarcodeOperations.GenerateTabletBarcodeBase64(SelectedTablet).image);
                Printer p = new Printer(template);
                p.ShowDialog();
            }
        }

        private void ChangeTabletLocation()
        {
            if (SelectedTablet !=null)
            {
                ChangeLocation location = new ChangeLocation(op, SelectedTablet);
                location.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wybierz tablet, który chcesz przekazać do innej lokalizacji");
            }
        }


        private void RemoteLock()
        {
            var result = MessageBox.Show("Tablet zostanie oznaczony jako uszkodzony, informacja zostanie przekazana do IT","Zdalna blokada",
                                 MessageBoxButton.YesNo,
                                MessageBoxImage.Question);
            if (SelectedTablet != null && result ==MessageBoxResult.Yes)
            {

                CommonDatabase.CommonDbAccess.Lock(SelectedTablet, CommonDatabase.Data.LockType.REMOTE_LOCK, op.ACPno, "Zablokowany zdalnie", DateTime.Now);
                NotifyPropertyChanged(nameof(Tablets));

            }
            else
            {
                MessageBox.Show("Wybierz tablet, który chcesz zablokować");
            }
        }

        private void AddComputer()
        {
            AddComputer computer = new AddComputer(op, vm);
            computer.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            computer.Show();
        }

    }

    
}
