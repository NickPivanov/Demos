using DesktopClient.Commands;
using DesktopClient.Models;
using DesktopClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class StartupViewModel : ObservableObject
    {
        private readonly StatisticsService carOrdersService;
        internal readonly IEnumerable<AnnualStatistics> sourceStatistic;
        
        public StartupViewModel(StatisticsService service)
        {
            carOrdersService = service;
            sourceStatistic = carOrdersService.GetStatistics();
            Initialize();

            PreviousPageCommand = new PreviousPageCommand();
            PreviousPageCommand.CanExecuteChanged += PreviousPageCommand_CanExecuteChanged;
            NextPageCommand = new NextPageCommand();
            NextPageCommand.CanExecuteChanged += NextPageCommand_CanExecuteChanged;
        }

#region Properties
        public List<object> YearsCollection { get; private set; }
        public List<string> CarModels { get; private set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        internal IEnumerable<AnnualStatistics> annualStatistics;
        public IEnumerable<AnnualStatistics> AnnualStatistics
        {
            get => annualStatistics;
            set { annualStatistics = value;
                OnPropertyChanged("annualStatistics"); }
        }

        internal string selectedModel;
        public string SelectedModel
        {
            get => selectedModel;
            set
            {
                if (selectedModel == value) return;
                selectedModel = value;
                this.FilterAnnualStatistics();
                OnPropertyChanged("selectedModel");
            }
        }

        //Data in UI supposed to be displayed divided on pages by year by default.
        //This property will allow to switch to the entire time period
        internal bool isEntirePeriod;
        public bool IsEntirePeriod 
        { get => isEntirePeriod; 
            set {
                isEntirePeriod = value;
                this.FilterAnnualStatistics();
                this.GetPage();
                OnPropertyChanged("isEntirePeriod");
            } 
        }

        //this property is required for filtering data
        internal string model => selectedModel switch
        {
            null => "",
            "All" => "",
            _ => selectedModel
        };
#endregion

#region Pagination
        internal int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("currentPage"); }
        } 

        internal int pages;
        public int Pages
        {
            get { return pages; }
            set { pages = value; OnPropertyChanged("pages"); }
        }

        private void PreviousPageCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            if (isEntirePeriod) return;
            
            if (currentPage <= (int)YearsCollection.Where(n => n.ToString() != "All").OrderByDescending(x => x).LastOrDefault())
                return;
            CurrentPage--;
            this.GetPage();
        }

        private void NextPageCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            if (isEntirePeriod) return;

            if (currentPage >= (int)YearsCollection.Where(n => n.ToString() != "All").OrderByDescending(x => x).FirstOrDefault()) 
                return;
            CurrentPage++;
            this.GetPage();
        }
#endregion

        private void Initialize()
        {
            AnnualStatistics = sourceStatistic;
            YearsCollection = AnnualStatistics.Select(n => n.Year).Distinct().Cast<object>().ToList();
            CarModels = AnnualStatistics.Select(n => n.CarModel).Distinct().ToList();
            pages = YearsCollection.Count(); //total amount of pages
            currentPage = YearsCollection.Any() is false ? 0 :
                (int)YearsCollection.OrderByDescending(x => x).First();
            YearsCollection.Insert(0, "All");
            CarModels.Insert(0, "All");
            isEntirePeriod = false;
            this.FilterAnnualStatistics();
        }
       

    }
}
