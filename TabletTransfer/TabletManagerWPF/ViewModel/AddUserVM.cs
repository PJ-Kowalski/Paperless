using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TabletManagerWPF.Helpers;
using static CommonDatabase.Data.Operator;

namespace TabletManagerWPF.ViewModel
{
    public class AddUserVM:BaseVM
    {
        public AddUserVM(in Operator Op, in MainWindowVM Vm)
        {
            this.op = Op;
            this.vm = Vm;
            if (vm.SelectedLocation == null) vm.SelectedLocation = op.Location;
            SelectedLocation = vm.SelectedLocation;
            SelectedPosition = PositionEnum.Normal;
            Positions = new List<PositionEnum>();
            Positions=CommonDatabase.CommonDbAccess.GetPositions().Where(x => x > op.Position).ToList();
        }

        #region Commands
        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand = new ExecuteCommand(() => SearchUser(), true);

        private ICommand _closeCommand;
        public ICommand CloseCommand => _closeCommand = new ExecuteCommand(() => Close(), true);

        private ICommand _addNewUserCommand;
        public ICommand AddNewUserCommand => _addNewUserCommand = new ExecuteCommand(() => AddUser(), true);

        #endregion

        #region Properites for view,

        private Operator _user;
        private readonly Operator op;
        private readonly MainWindowVM vm;

        public Operator User
        {
            get { return _user; }
            set { _user = value;
                NotifyPropertyChanged(nameof(User));
            }
        }

        public string ACPNo { get; set; }
        public PositionEnum SelectedPosition { get; set; }
        public string SelectedLocation { get; set; }
        public List<PositionEnum> Positions { get; set; }
        #endregion

        private void SearchUser()
        {
            User = CommonDatabase.CommonDbAccess.GetSomeone(ACPNo);
        }

        private void AddUser()
        {
            try
            {
                if (User != null && SelectedLocation != null)
                {
                    CommonDatabase.CommonDbAccess.AddOperatorToLocation(SelectedLocation, User.ACPno, SelectedPosition);
                    User = null;
                }
                else
                {
                    MessageBox.Show("Niekompletne dane");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("użytkownik istnierje już w bazie danych");
            }

        }

        private void Close()
        {
            Application.Current.Windows[1].Close();
        }
    }
}
