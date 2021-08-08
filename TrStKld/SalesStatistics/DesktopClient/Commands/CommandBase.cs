using System;
using System.Windows.Input;

namespace DesktopClient.Commands
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CanExecuteChanged.Invoke(this, null);
        }
    }
}
