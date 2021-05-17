using DesktopClient.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace DesktopClient.Commands.MotorsViewCommands
{
    public class ShowMotorFormCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            Window motorForm = App.serviceProvider.GetRequiredService<MotorFormView>();
            motorForm.Show();
            base.Execute(parameter);
        }
    }
}
