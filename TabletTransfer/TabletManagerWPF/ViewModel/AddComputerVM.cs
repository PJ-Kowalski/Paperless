using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TabletManagerWPF.Helpers;

namespace TabletManagerWPF.ViewModel
{
    class AddComputerVM:BaseVM
    {
        private readonly Operator op;
        private readonly MainWindowVM vm;

        public AddComputerVM(in Operator Op, in MainWindowVM Vm)
        {
            op = Op;
            vm = Vm;
            if (vm.SelectedLocation == null) vm.SelectedLocation = op.Location;
        }

        #region Commands
        private ICommand _addComputerhCommand;
        public ICommand AddComputerCommand => _addComputerhCommand = new ExecuteCommand(() => AddComputer(), true);

        private ICommand _cancelCommand;
       

        public ICommand CancelCommand => _cancelCommand = new ExecuteCommand(() => Close(), true);


        #endregion

        #region properites for View
        private string _computerName;

        public string ComputerName
        {
            get { return _computerName; }
            set { _computerName = value;
                NotifyPropertyChanged(nameof(ComputerName)); }
        }

        #endregion

        private void AddComputer()
        {
            if (ComputerName!=null && !ComputerName.Contains(" "))
            {
                CommonDatabase.CommonDbAccess.AddComputer(ComputerName, op.ACPno, vm.SelectedLocation);
                MessageBox.Show($"Komputer {ComputerName} xostał dodany do lokalizacji {vm.SelectedLocation}");
                ComputerName = "";
            }
            else
            {
                MessageBox.Show("Niepoprawna nazwa komputera");
            }
        }

        private void Close()
        {
            Application.Current.Windows[1].Close();
        }
    }
}
