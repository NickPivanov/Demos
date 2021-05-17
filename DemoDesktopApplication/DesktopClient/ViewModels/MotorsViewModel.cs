using DesktopClient.Commands.MotorsViewCommands;
using Domain.Models;
using Domain.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class MotorsViewModel : ViewModelBase
    {
        private readonly IMotorService<Motor> motorService;
        public MotorsViewModel(IMotorService<Motor> service)
        {
            motorService = service;

            saveMotorChangesCommand = new SaveChangesCommand(motorService);
            saveMotorChangesCommand.CanExecuteChanged += (s, e) => Task.Run(async () => await GetData());
            deleteMotorCommand = new DeleteCommand(motorService);
            deleteMotorCommand.CanExecuteChanged += (s, e) => Task.Run(async () => await GetData());
            showMotorFormCommand = new ShowMotorFormCommand();
            refreshDataCommand = new RefreshDataCommand(() => Task.Run(async () => await GetData()));
        }

        private ObservableCollection<Motor> motors { get; set; }
        public ObservableCollection<Motor> Motors
        {
            get { return motors; }
            set { motors = value; OnPropertyChanged("motors"); }
        }

        private Motor selectedMotor { get; set; }
        public Motor SelectedMotor
        {
            get { return selectedMotor; }
            set
            {
                selectedMotor = value;
                OnPropertyChanged("selectedMotor");
            }
        }

        public ICommand saveMotorChangesCommand { get; set; }
        public ICommand deleteMotorCommand { get; set; }
        public ICommand showMotorFormCommand { get; set; }
        public ICommand refreshDataCommand { get; set; }

        private async Task GetData()
        {
            var listofmotors = await motorService.GetAllAsync();
            Motors = new ObservableCollection<Motor>(listofmotors);
            MotorsCollection = Motors;
        }

    }
}
