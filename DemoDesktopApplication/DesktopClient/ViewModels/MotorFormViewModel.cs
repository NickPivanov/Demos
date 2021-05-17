using DesktopClient.Commands.MotorFormViewCommands;
using Domain.Models;
using Domain.Services;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class MotorFormViewModel : ViewModelBase
    {

        private readonly IMotorService<Motor> motorService;
        private readonly IMotorPropertyService<MotorProperty> propertyService;

        public MotorFormViewModel(IMotorService<Motor> service, IMotorPropertyService<MotorProperty> PropertyService)
        {
            motorService = service;
            propertyService = PropertyService;
            motor = new() { Name = "Name" };

            AddMotorCommand = new AddMotorCommand(motorService, propertyService);
            AddMotorCommand.CanExecuteChanged += (s, e) => MotorsCollection.Add(Motor);
        }

        private Motor motor { get; set; }
        public Motor Motor
        {
            get { return motor; }
            set
            {
                motor = value;
                OnPropertyChanged("motor");
            }
        }

        public ICommand AddMotorCommand { get; set; }
    }
}
