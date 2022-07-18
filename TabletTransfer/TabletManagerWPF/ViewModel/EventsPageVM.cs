using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TabletManagerWPF.Helpers;

namespace TabletManagerWPF.ViewModel
{
    public delegate void IsAlertActive(bool alarmIsActive);
    internal class EventsPageVM : BaseVM
    {
        public Operator Op { get; }
        public MainWindowVM Vm { get; }
        const int preloadAlarmsDaysFrom = -14;

        CancellationTokenSource cancelToken;
        //CancellationToken token;
        static ManualResetEventSlim mres = new ManualResetEventSlim(false);
        public EventsPageVM(in Operator op, in MainWindowVM vm)
        {
            Events = new List<TabletEvent>();
            Alerts =new List<Alert>();
            Op = op;
            Vm = vm;
            AutoRefresch = true;
            Vm.OnLocationChanged += Vm_OnLocationChanged;
            Events = CommonDatabase.CommonDbAccess.GetHistoryForLocation(vm.SelectedLocation, DateTime.Now.AddDays(preloadAlarmsDaysFrom), DateTime.Now);
            Alerts = CommonDatabase.CommonDbAccess.GetAlertsForLocation(vm.SelectedLocation, DateTime.Now.AddDays(preloadAlarmsDaysFrom));
            EventsFrom = DateTime.Now.AddDays(preloadAlarmsDaysFrom);
            EventsTo= DateTime.Now;
            AlertsFrom = DateTime.Now.AddDays(preloadAlarmsDaysFrom);
            AlertsTo= DateTime.Now;
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

        }
            

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (AutoRefresch)
            {
                cancelToken = new CancellationTokenSource();
                CancellationToken token = cancelToken.Token;

                Task t1 = Task.Run(() =>
                {
                    List<Alert> tempAlert = new List<Alert>(CommonDatabase.CommonDbAccess.GetAlertsForLocation(Vm.SelectedLocation, AlertsFrom));
                    bool match = false;
                    foreach (var item in tempAlert)
                    {
                        var l = tempAlert.AsParallel().Select(x => x).Where(x => x.AckDate != item.AckDate && x.Id == item.Id);
                        if (l.Count() > 0)
                        { match = true; break; }
                    }
                    if (tempAlert.Count != Alerts.Count)
                    {
                        match = true;
                    }
                    if (match)
                    {
                       Alerts = tempAlert;
                    }
                    tempAlert=null;
                },token);


                Task t2 = Task.Factory.StartNew(() =>
                {
                    List<TabletEvent> tempEvent = new List<TabletEvent>(CommonDatabase.CommonDbAccess.GetHistoryForLocation(Vm.SelectedLocation, EventsFrom, DateTime.Now));
                    bool match = false;
                    if (tempEvent.Count != Events.Count)
                    {
                        match = true;
                    }
                    if (match)
                    {
                        Events = tempEvent;
                    }
                    tempEvent = null;
                },token);


            }

            if (Alerts.Select(x => x).Where(x => x.AckBy == null).Any())
                { AlertCalback(true); }
            else 
                { AlertCalback(false); }
            GC.Collect();

        }

        private void Vm_OnLocationChanged(string location)
        {
            Events = CommonDatabase.CommonDbAccess.GetHistoryForLocation(Vm.SelectedLocation, DateTime.Now.AddDays(preloadAlarmsDaysFrom), DateTime.Now).Select(x=>x).Where(x=>x.Location == location).ToList();
            Alerts = CommonDatabase.CommonDbAccess.GetAlertsForLocation(Vm.SelectedLocation, DateTime.Now.AddDays(preloadAlarmsDaysFrom)).Select(x => x).Where(x => x.Location == location).ToList();
        }

        #region Commands
        private System.Windows.Input.ICommand _confirmAlarmCommand;
        public System.Windows.Input.ICommand ConfirmAlarmCommand => _confirmAlarmCommand = new ExecuteCommand(() => ConfirmAlert(), true);

        private System.Windows.Input.ICommand _refreshAlarmsCommand;
        public System.Windows.Input.ICommand RefreshAlarmsCommand => _refreshAlarmsCommand = new ExecuteCommand(() => RefreshAlerts(), true);

        private System.Windows.Input.ICommand _refreshEventsCommand;
        public System.Windows.Input.ICommand RefreshEventsCommand => _refreshEventsCommand = new ExecuteCommand(() => RefreshEvents(), true);
        #endregion

        #region properites for view

        private List<TabletEvent> _events;
        public List<TabletEvent> Events
        {
            get { return _events; }
            set { _events = value;
                NotifyPropertyChanged(nameof(Events)); }
        }

        private List<Alert> _alerts;
        public List<Alert> Alerts
        {
            get { return _alerts; }
            set { _alerts = value;
                NotifyPropertyChanged(nameof(Alerts)); }
        }

        public Alert SelectedAlert { get; set; }


        private bool _oneHourEvent;
        public bool OneHourEvent
        {
            get { return _oneHourEvent; }
            set
            {
                _oneHourEvent = value;
                if (value == true) {
                    EventsFrom = DateTime.Now.AddHours(-1);
                    EventsTo = DateTime.Now;
                    RefreshEvents(); };
            }
        }

        private bool _tvelweHourEvent;
        public bool TvelweHourEvent
        {
            get { return _tvelweHourEvent; }
            set
            {
                _tvelweHourEvent = value;
                if (value == true) {
                    EventsFrom = DateTime.Now.AddHours(-12);
                    EventsTo = DateTime.Now;
                    RefreshEvents(); };
            }
        }
        private bool _oneDayEvent;
        public bool OneDayEvent
        {
            get { return _oneDayEvent; }
            set
            {
                _oneDayEvent = value;
                if (value == true) {
                    EventsFrom = DateTime.Now.AddDays(-1);
                    EventsTo = DateTime.Now;
                    RefreshEvents(); };
            }
        }

        private bool _rangeEvent;
        public bool RangeEvent
        {
            get { return _rangeEvent; }
            set
            {
                _rangeEvent = value;
                if (value == true)
                {
                    RefreshEvents();
                };
            }
        }


        private bool _oneHourAlert;
        public bool OneHourAlert
        {
            get { return _oneHourAlert; }
            set
            {
                _oneHourAlert = value;
                if (value == true) {
                    AlertsFrom = DateTime.Now.AddHours(-1);
                    RefreshAlerts(); };
            }
        }

        private bool _tvelweHourAlert;
        public bool TvelweHourAlert
        {
            get { return _tvelweHourAlert; }
            set
            {
                _tvelweHourAlert = value;
                if (value == true) {
                    AlertsFrom = DateTime.Now.AddHours(-12);
                    RefreshAlerts(); };
            }
        }
        private bool _oneDayAlert;
        public bool OneDayAlert
        {
            get { return _oneDayAlert; }
            set
            {
                _oneDayAlert = value;
                if (value == true) {
                    AlertsFrom = DateTime.Now.AddDays(-1);
                    RefreshAlerts(); };
            }
        }


        private bool _rangeAlert;
        public bool RangeAlert
        {
            get { return _rangeAlert; }
            set
            {
                _rangeAlert = value;
                if (value == true)
                {
                    RefreshEvents();
                };
            }
        }

        public DateTime EventsFrom { get; set; }
        public DateTime EventsTo { get; set; }
        public DateTime AlertsFrom { get; set; }
        public DateTime AlertsTo { get; set; }
        public bool AutoRefresch { get; set; }
        #endregion

        private void RefreshEvents()
        {
            Events = CommonDatabase.CommonDbAccess.GetHistoryForLocation(Vm.SelectedLocation, EventsFrom, EventsTo);
        }

        private void RefreshAlerts()
        {
            Alerts = CommonDatabase.CommonDbAccess.GetAlertsForLocation(Vm.SelectedLocation, AlertsFrom);
        }

        private void ConfirmAlert()
        {
            cancelToken?.Cancel();
            AutoRefresch = false;
            if (SelectedAlert !=null)
            {
                Task t3 =  Task.Run(() =>
                {
                    CommonDatabase.CommonDbAccess.AckAlert(SelectedAlert, Op);
                    RefreshAlerts();
                });
                GC.Collect();
            }
            cancelToken?.Dispose();
            cancelToken = null;
            AutoRefresch= true;

        }

        private void AlertCalback(bool alarmIsActiv)
        {
            //Alerts collback to MainWindowVM
            MethodInfo m1 = typeof(MainWindowVM).GetMethod(nameof(MainWindowVM.OnAlertActive), BindingFlags.Public | BindingFlags.Instance);
            //IsAlertActive alertActiveDelegate;
            Delegate test = Delegate.CreateDelegate(typeof(IsAlertActive), Vm, m1, false);
            test.DynamicInvoke(alarmIsActiv);
            //alertActiveDelegate = (IsAlertActive)test;
            //alertActiveDelegate(alarmIsActiv);
        }

    }
}
 