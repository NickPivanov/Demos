using Microsoft.Extensions.Logging;
using System;
using System.Windows.Input;

namespace DesktopClient.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            CanExecuteChanged.Invoke(this, null);
        }
    }
}
