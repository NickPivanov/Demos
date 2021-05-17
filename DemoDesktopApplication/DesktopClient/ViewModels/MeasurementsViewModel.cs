using DesktopClient.Commands.MeasurementsViewCommands;
using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class MeasurementsViewModel : ViewModelBase
    {
        private readonly IMeasurementService<Measurement> measurementService;
        public MeasurementsViewModel(IMeasurementService<Measurement> service)
        {
            measurementService = service;
            Task.Run(async () => await GetData());

            PreviousPageCommand = new PreviousPageCommand();
            PreviousPageCommand.CanExecuteChanged += PreviousPageCommand_CanExecuteChanged;
            NextPageCommand = new NextPageCommand();
            NextPageCommand.CanExecuteChanged += NextPageCommand_CanExecuteChanged;
        }

        private IEnumerable<Measurement> measurements;
        public IEnumerable<Measurement> Measurements
        {
            get { return measurements; }
            set { measurements = value; OnPropertyChanged("measurements"); }
        }

        private List<string> motorNames;
        public List<string> MotorNames 
        {
            get { return motorNames; }
            set { motorNames = value; OnPropertyChanged("motorNames"); }
        }

        private string selectedMotor;
        public string SelectedMotor
        {
            get { return selectedMotor; }
            set { 
                    if(selectedMotor == value) return;
                    selectedMotor = value;
                    FilterMeasurementsByMotorName();
                    OnPropertyChanged("selectedMotor"); 
                }
        }

        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        
        private int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("currentPage"); }
        }

        private int pages;
        public int Pages
        {
            get { return pages; }
            set { pages = value; OnPropertyChanged("pages"); }
        }

        private static IEnumerable<Measurement> sourceMeasurementCollection;
       
        private async Task GetData()
        {
            sourceMeasurementCollection = await measurementService.GetMeasurementsAsync();
            currentPage = 1;
            pages = sourceMeasurementCollection.Count() / 7;
            MotorNames = sourceMeasurementCollection.Select(m => m.Motor.Name).Distinct().ToList();
            MotorNames.Insert(0,"All");
            Measurements = sourceMeasurementCollection.Skip(7*(currentPage-1)).Take(7);
        }

        private void PreviousPageCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            if (currentPage <= 1) return;
            CurrentPage--;
            GetMeasurementsPage();
        }

        private void NextPageCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            if (currentPage >= pages) return;
            CurrentPage++;
            GetMeasurementsPage();
        }

        private void FilterMeasurementsByMotorName()
        {
            if (selectedMotor == "All")
            { 
                Measurements = sourceMeasurementCollection.Take(7);
                Pages = sourceMeasurementCollection.Count() / 7;
            }
            else
            {
                Measurements = sourceMeasurementCollection.Where(m => m.Motor.Name == selectedMotor).Take(7);
                Pages = Measurements.Count() / 7;
            }
            CurrentPage = 1;
        }

        private void GetMeasurementsPage()
        {
            Measurements = SelectedMotor == "All" ?
                sourceMeasurementCollection.Skip(7 * (currentPage - 1)).Take(7) :
                sourceMeasurementCollection.Where(m => m.Motor.Name == SelectedMotor).Skip(7 * (currentPage - 1)).Take(7);
        }
    }
}
