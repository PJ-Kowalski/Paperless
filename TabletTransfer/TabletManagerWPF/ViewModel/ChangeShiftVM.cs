using CommonDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabletManagerWPF.Helpers;
using TabletManagerWPF.View;
using static CommonDatabase.Data.Operator;

namespace TabletManagerWPF.ViewModel
{
    internal class ChangeShiftVM:BaseVM
    {

        public Operator Op { get; }
        private readonly ChangeShift view;

        public ChangeShiftVM(in Operator op, in ChangeShift view)
        {

            Tablets = new List<Tablet>();
            Tablets = CommonDatabase.CommonDbAccess.GetTablets(op.Location, op);
            Op = op;
            VisibilityElements = false;
            this.view = view;
            view.OnPasswordChanged += View_OnPasswordChanged;
        }

        private void View_OnPasswordChanged(string password)
        {
            ReciverPassword= password;
        }

        #region Commands

        private System.Windows.Input.ICommand _authorizeCommand;
        public System.Windows.Input.ICommand AuthorizeCommand => _authorizeCommand = new ExecuteCommand(() => Authorize(), true);

        private System.Windows.Input.ICommand _changeShiftCommand;
        public System.Windows.Input.ICommand ChangeShiftCommand => _changeShiftCommand = new ExecuteCommand(() => ChangeShift(), true);

    

        #endregion

        #region Properites for view
        private Operator user;
        public Operator User
        {
            get { return user; }
            set { user = value;
                NotifyPropertyChanged(nameof(User));
            }
        }

        private List<Tablet> _tablets;
        public List<Tablet> Tablets
        {
            get { return _tablets; }
            set
            {
                _tablets = value;
                NotifyPropertyChanged(nameof(Tablets));
            }
        }

        private int _tabletNumberConfirmation;

        public int TabletNumberConfirmation
        {
            get { return _tabletNumberConfirmation; }
            set { _tabletNumberConfirmation = value; }
        }

        private string _reciverAcpNumber;
        public string ReciverAcpNumber
        {
            get { return _reciverAcpNumber; }
            set { _reciverAcpNumber = value; }
        }

        private string _reciverPassword;
        

        public string ReciverPassword
        {
            get { return _reciverPassword; }
            set { _reciverPassword = value; }
        }


        private bool visibilityElements;
        public bool VisibilityElements
        {
            get { return visibilityElements; }
            set
            {
                visibilityElements = value;
                NotifyPropertyChanged(nameof(VisibilityElements));
            }
        }


        #endregion

        private void ChangeShift()
        {
            if (TabletNumberConfirmation == Tablets.Count)
            {
                foreach (var tablet in Tablets)
                {
                    CommonDatabase.CommonDbAccess.PassTheDevice(tablet, User, false, null, "MAGAZYN");
                }; 
            view.Close();
            }
            else
            {
                MessageBox.Show("Wprowadzono niepoprawną liczbę tabletów do przekazania");
            }

        }

        private void Authorize()
        {
            Operator tmp = new Operator();
            tmp = CommonDatabase.CommonDbAccess.GetOperator(ReciverAcpNumber, Op.Location, ReciverPassword);
            if (tmp!=null && tmp.PasswordVerified==PasswordVerificationResult.OK)
            {
                User = tmp;
                VisibilityElements = true;
            }
            else
            {
                MessageBox.Show("Nieprawidłowe dane użytkownika");
            }
        }

    }
}
