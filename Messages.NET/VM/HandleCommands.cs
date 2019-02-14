using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Messages.NET
{
    class HandleCommands : ICommand
    {
        private readonly Action _actionToExecute;

        public event EventHandler CanExecuteChanged;

        public HandleCommands(Action action)
        {
            _actionToExecute = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _actionToExecute();
        }
    }
}
