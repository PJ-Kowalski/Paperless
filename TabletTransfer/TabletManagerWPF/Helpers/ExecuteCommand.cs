using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TabletManagerWPF.Helpers
{
    class ExecuteCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private bool _canexecute;
        private Action _command;
        public ExecuteCommand(Action Command, bool CanExecute)
        {
            _canexecute = CanExecute;
            _command = Command;
        }

        public bool CanExecute(object parameter)
        {
            return _canexecute;
        }

        public void Execute(object parameter)
        {
            _command();
        }


    }
}
