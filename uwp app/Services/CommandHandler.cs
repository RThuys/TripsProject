using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace uwp_app.Services
{
    class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;

        public CommandHandler(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action();
        }
    }
}
