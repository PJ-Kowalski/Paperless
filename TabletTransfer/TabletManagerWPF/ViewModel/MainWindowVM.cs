using CommonDatabase.Data;
using CommonDatabase.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TabletManagerWPF.Helpers;
using TabletManagerWPF.View;
using static TabletManagerWPF.ViewModel.EventsPageVM;

namespace TabletManagerWPF.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        public delegate void LocationChanged(string location);
        public event LocationChanged OnLocationChanged;
        public Page UserPage { get; set; }
        public Page TabletPage { get; set; }
        public Page EventsPage { get; set; }
        public Page SetingPage { get; set; }

        public MainWindowVM(in Operator op)
        {
            User = op ?? throw new ArgumentNullException(nameof(op));
            CurrentPageModel = new Page();
            SelectedLocation = op.Location;
            UserPage = new View.Pages.UsersPage(User,  this);
            TabletPage = new View.Pages.TabletsPage(User, this);
            EventsPage = new View.Pages.EventsPage(User, this);
            SetingPage= new View.Pages.SettingPage(User, this);
            CurrentPageModel = UserPage;
            Locations = CommonDatabase.CommonDbAccess.GetLocationsForSomeone(op).ToList();
            GetPhotosToLocalStorage();
            AppVersion ="ver. "+ System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            
        }

        //method used in delegate, in EventPageVM Class- to notyfy unacknowledged alarms
        public void OnAlertActive(bool alarmIsAcitve)
        {
            AlarmIsActiv = alarmIsAcitve;
        }

        private Page currentViewModel;
        public Page CurrentPageModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                NotifyPropertyChanged(nameof(CurrentPageModel));
            }
        }


        #region properites for view

        public string AppVersion { get; set; }

        private bool _alarmIsActive;
        public bool AlarmIsActiv
        {
            get { return _alarmIsActive; }
            set { _alarmIsActive = value;
                NotifyPropertyChanged(nameof(AlarmIsActiv)); }
        }


        public Operator User { get; set; }
        public List<Location> Locations { get; set; }
        private string _selectedLoaction;
        public string SelectedLocation
        {
            get { return _selectedLoaction; }
            set { _selectedLoaction = value;
                NotifyPropertyChanged(nameof(SelectedLocation));
                if (OnLocationChanged!=null)
                {
                 OnLocationChanged(SelectedLocation);
                }
            }
        }

        #endregion

        #region Commands
        //command properity
        private ICommand _loadUserPageCommand;
        public ICommand LoadUserPageCommand => _loadUserPageCommand = new ExecuteCommand(() => LoadUserPage(), true);

        private ICommand _loadTabletsPageCommand;
        public ICommand LoadUserTabletsCommand => _loadTabletsPageCommand = new ExecuteCommand(() => LoadTabletsPage(), true);

        private ICommand _loadEventsPageCommand;
        public ICommand LoadEventsPageCommand => _loadEventsPageCommand = new ExecuteCommand(() => LoadEventsPage(), true);

        private ICommand _loadSetingPageCommand;
        public ICommand LoadSetingPageCommand => _loadSetingPageCommand = new ExecuteCommand(() => LoadSetingPage(), true);

        private ICommand _changeShiftCommand;
        public ICommand ChangeShiftCommand => _changeShiftCommand = new ExecuteCommand(() => OpenChangeShiftWindow(), true);

        //private ICommand _TestCommand;
        //public ICommand TestCommand => _TestCommand = new ExecuteCommand(() => TEST(), true);
        #endregion

        private void GetPhotosToLocalStorage()
        {
            List<string> list = CommonDatabase.CommonDbAccess.GetUserNameListForLocation(SelectedLocation);
            if (!Directory.Exists(@"c:\Photos"))
            {
                Directory.CreateDirectory(@"c:\Photos");
            }
            PhotoDownloader.GetPhotosForLocation(list);
        }

        private void LoadUserPage()
        {
            CurrentPageModel = UserPage;
        }

        private void LoadTabletsPage()
        {
            CurrentPageModel = TabletPage;
        }

        private void LoadEventsPage()
        {
            CurrentPageModel = EventsPage;
        }

        private void LoadSetingPage()
        {
            CurrentPageModel = SetingPage;
        }

        private void OpenChangeShiftWindow()
        {
            ChangeShift shift = new ChangeShift(User);
            shift.ShowDialog();
        }
    }
}
