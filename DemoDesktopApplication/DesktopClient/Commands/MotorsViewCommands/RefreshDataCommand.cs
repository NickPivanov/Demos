using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Commands.MotorsViewCommands
{
    public class RefreshDataCommand : BaseCommand
    {
        Action refreshAction;
        public RefreshDataCommand(Action action)
        {
            refreshAction = action;
        }
        public override void Execute(object parameter)
        {
            refreshAction.Invoke();
            base.Execute(parameter);
        }
    }
}
