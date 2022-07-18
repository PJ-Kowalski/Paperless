using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabletManagerWPF.Helpers;
using TabletManagerWPF.View;

namespace TabletManagerWPF.ViewModel
{
    internal class ChangeLocationVM : BaseVM
    {
        private readonly ChangeLocation view;
        public ChangeLocationVM(Operator op, CommonDatabase.Data.Tablet selectedTablet, ChangeLocation view)
        {
            Oper = op;
            SelectedTablet = selectedTablet;
            this.view = view;
            view.OnPasswordChanged += View_OnPasswordChanged;
            Locations = CommonDatabase.CommonDbAccess.GetAllLocations().ToList();
        }

        private void View_OnPasswordChanged(string Password)
        {
            PasswordReciver = Password;
        }

        #region Commands
        //command properity
        private ICommand _changeLocationCommand;
        public ICommand ChangeLocationCommand
        {
            get { return _changeLocationCommand = new ExecuteCommand(() => ChangeTabletLocation(), true); }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get { return _cancelCommand = new ExecuteCommand(() => Cancel(), true); }
        }

        #endregion

        #region Properites for view

        public CommonDatabase.Data.Tablet SelectedTablet { get; set; }
        public Operator Oper { get; set; }
        public string ACPReciver { get; set; }
        public string PasswordReciver { get; set; }
        public List<Location> Locations { get; set; }
        public string SelectedLocation { get; set; }
        #endregion

        #region Methods

        private void ChangeTabletLocation()
        {
            Operator op = CommonDatabase.CommonDbAccess.GetOperator(ACPReciver, SelectedLocation, PasswordReciver);
            if (op != null)
            {
                CommonDatabase.CommonDbAccess.UpdateTabletLocation(SelectedTablet.WindowsName, ACPReciver, SelectedLocation);
                CommonDatabase.CommonDbAccess.RegisterEvent(SelectedTablet, ACPReciver, "LOCALIZATION_CHANGED", $"tablet changed location to {SelectedLocation} by {ACPReciver}");
                view.Close();
            }

        }

        private void Cancel()
        {
            view.Close();
        }

        #endregion


    }
}
