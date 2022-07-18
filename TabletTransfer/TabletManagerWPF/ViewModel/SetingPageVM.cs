using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabletManagerWPF.Helpers;

namespace TabletManagerWPF.ViewModel
{
    public class SetingPageVM:BaseVM
    {
        public Operator Op { get; }
        public MainWindowVM Vm { get; }

        public SetingPageVM(in Operator op,in MainWindowVM vm)
        {
            Op = op;
            Vm = vm;
            CheckUserRights();
            UserSetings = new ObservableCollection<UserSeting>();
            Locations = CommonDatabase.CommonDbAccess.GetLocationsForSomeone(op).ToList();
            RefreshSettings();
        }


        #region Commands
        private ICommand _addNotificationReciverCommand;
        public ICommand AddNotificationReciverCommand => _addNotificationReciverCommand = new ExecuteCommand(() => AddNotificationReciver(), true);

        private ICommand _removeNotificationReciverCommand;
        public ICommand RemoveNotificationReciverCommand => _removeNotificationReciverCommand = new ExecuteCommand(() => RemoveNotificationReciver(), true);

        private ICommand _saveChangesCommand;
        public ICommand SaveChangesCommand => _saveChangesCommand = new ExecuteCommand(() => SaveChanges(), true);

        #endregion

        #region Properites for view

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value;
                NotifyPropertyChanged(nameof(Email));
            }
        }

        public List<Location> Locations { get; set; }
        public Location SelectedLocation { get; set; }

        private ObservableCollection<UserSeting> _userSetings;
        public ObservableCollection<UserSeting> UserSetings
        {
            get { return _userSetings; }
            set { _userSetings = value;
                NotifyPropertyChanged(nameof(UserSetings));
            }
        }

        public UserSeting SelectedSeting { get; set; }
        public bool configurationVisible { get; set; }

        #endregion

        #region Methods

        private void AddNotificationReciver()
        {
            if (EmailValidator.IsValidEmail(Email))
            {
                UserSeting set = new UserSeting() { Category ="MAIL", Seting1=Email, Seting2 =SelectedLocation.Name};
                CommonDatabase.CommonDbAccess.AddUserSeting(set);
                Email = null;
                RefreshSettings();
            };
        }

        private void RemoveNotificationReciver()
        {
            CommonDatabase.CommonDbAccess.RemoveUserSeting(SelectedSeting);
            RefreshSettings();
        }

        private void SaveChanges()
        {

        }

        private void RefreshSettings()
        {
            UserSetings.Clear();
            foreach (var item in Locations)
            {
                foreach (var entry in CommonDatabase.CommonDbAccess.GetUserSetings(item.Name).Select(x=>x).Where(x=>x.Category=="MAIL"))
                {
                    UserSetings.Add(entry);
                }
            }
        }
        #endregion


        private void CheckUserRights()
        {
            if (Op.Position==Operator.PositionEnum.TL || Op.Position==Operator.PositionEnum.Service)
            {
                configurationVisible = true;
            }
        }

    }
}
