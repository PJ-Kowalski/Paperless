using CommonDatabase.Data;
using CommonDatabase.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TabletManagerWPF.Helpers;
using TabletManagerWPF.View;
using TabletManagerWPF.View.UserControls;
using static CommonDatabase.Data.Operator;

namespace TabletManagerWPF.ViewModel
{
    public class UserPageVM : BaseVM
    {
        private readonly Operator op;
        private readonly MainWindowVM vm;
        CancellationTokenSource cancelToken;
        //CancellationToken token;

        public UserPageVM(in Operator Op, in MainWindowVM Vm)
        {
            Vm.OnLocationChanged += Main_OnLocationChanged;

            this.op = Op;
            this.vm = Vm;
            if (vm.SelectedLocation == null) vm.SelectedLocation = op.Location;

            Operators = new List<Operator>();
            Operators = CommonDatabase.CommonDbAccess.GetOperators(vm.SelectedLocation, op.GetUnderlings());
            Test = op.ACPno.ToString();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (isAllFilterIsSelected)
            {
                cancelToken = new CancellationTokenSource();
                CancellationToken token = cancelToken.Token;

                Task.Run(() =>
                {
                    List<Operator> tempOperator = new List<Operator>(CommonDatabase.CommonDbAccess.GetOperators(vm.SelectedLocation, op.GetUnderlings()));
                    bool match = false;
                    foreach (var item in Operators)
                    {
                        var l = tempOperator.AsParallel().Select(x => x).Where(x => (x.ACPno == item.ACPno) && (x.IsOnPremiseStr != item.IsOnPremiseStr));
                                                                     
                        if (l.Count() > 0)
                        { match = true; break; }
                    }
                    if (tempOperator.Count != Operators.Count)
                    {
                        match = true;
                    }
                    if (match)
                    {
                        Operators = tempOperator;
                    }
                    tempOperator=null;   
                },token);

                cancelToken.Dispose();
                cancelToken = null; 
                GC.Collect();
            }
        }

        private  void Main_OnLocationChanged(string location)
        {
            //Cancelation token cancels task coled by DispatcherTimer
            cancelToken?.Cancel();
            List<Operator>tmp = new List<Operator>();
            if (Operators != null)
            {
                Operators.Clear();
                Task t1 = Task<List<Operator>>.Factory.StartNew(() =>{
                    return tmp = CommonDatabase.CommonDbAccess.GetOperators(location, op.GetUnderlings());},TaskCreationOptions.LongRunning)
                    .ContinueWith(t=>Operators =t.Result);

            }
            GC.Collect();
        }

        public string Test { get; set; }

        #region Commands

        //private ICommand _removeUserCommand;
        //public ICommand RemoveUserCommand => _removeUserCommand = new ExecuteCommand(() => LoadUserPage(), true);

        private ICommand _operatorFilterCommand;
        public ICommand OperatorFilterCommand => _operatorFilterCommand = new ExecuteCommand(() => ListOperatorsFilter(), true);

        private ICommand _SLFilterCommand;
        public ICommand SLFilterCommand => _SLFilterCommand = new ExecuteCommand(() => ListSLFilter(), true);

        private ICommand _TLFilterCommand;
        public ICommand TLFilterCommand => _TLFilterCommand = new ExecuteCommand(() => ListTLFilter(), true);

        private ICommand _ServiceFilterCommand;
        public ICommand ServiceFilterCommand => _ServiceFilterCommand = new ExecuteCommand(() => ListServiceFilter(), true);

        private ICommand _AllFilterCommand;
        public ICommand AllFilterCommand => _AllFilterCommand = new ExecuteCommand(() => ListAllFilter(), true);

        private ICommand _showAddUserCommand;
        public ICommand ShowAddUserCommand => _showAddUserCommand = new ExecuteCommand(() => ShowAddUser(), true);

        private ICommand _removerUserCommand;
        public ICommand RemoveUserCommand => _removerUserCommand = new ExecuteCommand(() => RemoveUserUser(), true);

        private ICommand _printTemporaryBadgeCommand;
        public ICommand PrintTemporaryBadgeCommand => _printTemporaryBadgeCommand = new ExecuteCommand(() => PrintTemporaryBadge(), true);



        #endregion

        #region properites for view

        public Operator SelectedOperator { get; set; }
        public bool isAllFilterIsSelected { get; set; } = true; 
        private List<Operator> _operators;
        public List<Operator> Operators
        {
            get { return _operators; }
            set
            {
                _operators = value;
                NotifyPropertyChanged(nameof(Operators));
            }
        }

        #endregion


        private void ListOperatorsFilter()
        {
            var oper = CommonDatabase.CommonDbAccess.GetOperators(vm.SelectedLocation, op.GetUnderlings());
            Operators = oper.Where(x => x.Position == PositionEnum.Normal).ToList();
        }
       
        private void ListSLFilter()
        {
            var oper = CommonDatabase.CommonDbAccess.GetOperators(vm.SelectedLocation, op.GetUnderlings());
            Operators = oper.Where(x => x.Position == PositionEnum.SL).ToList();
        }

        private void ListTLFilter()
        {
            var oper = CommonDatabase.CommonDbAccess.GetOperators(vm.SelectedLocation, op.GetUnderlings());
            Operators = oper.Where(x => x.Position == PositionEnum.TL).ToList();
        }

        private void ListServiceFilter()
        {
            var oper = CommonDatabase.CommonDbAccess.GetOperators(vm.SelectedLocation, op.GetUnderlings());
            Operators = oper.Where(x => x.Position == PositionEnum.Service).ToList();
        }

        private void ListAllFilter()
        {
            var oper = CommonDatabase.CommonDbAccess.GetOperators(vm.SelectedLocation, op.GetUnderlings());
            Operators = oper;
        }

        private void ShowAddUser()
        {
            AddUser user = new AddUser(op, vm);
            user.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            user.Show();
        }

        private void RemoveUserUser()
        {
            CommonDatabase.CommonDbAccess.RemoveOperatorFromLocation(vm.SelectedLocation, SelectedOperator);
            Operators = CommonDatabase.CommonDbAccess.GetOperators(vm.SelectedLocation, op.GetUnderlings());
        }

        private void PrintTemporaryBadge()
        {
            if (SelectedOperator!=null)
            {
                var now = DateTime.Now;
                var expiration_date = CommonDatabase.CommonDbAccess.AddTemporaryBadge(SelectedOperator, now, op);
                if (expiration_date.HasValue)
                {
                    string template = File.ReadAllText(Path.Combine("Resources", "badge.html"))
                            .Replace("[NAME]", SelectedOperator.Name.ToString())
                            .Replace("[ACPNO]", SelectedOperator.ACPno.ToString())
                            .Replace("[EXPIRATION]", expiration_date.Value.ToString("yyyy-MM-dd HH:mm"))
                            .Replace("[BARCODE]", BarcodeOperations.GenerateOperatorBarcodeBase64(SelectedOperator, now));
                    Printer p = new Printer(template);
                    p.ShowDialog();
                } 
            }

        }
    }
}
