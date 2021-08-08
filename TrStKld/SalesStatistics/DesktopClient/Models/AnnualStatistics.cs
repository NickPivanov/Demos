using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopClient.Models
{
    //Final model to represent data for UI
    public class AnnualStatistics 
    {
        public AnnualStatistics(List<CarOrdersModel> statistics, string CarModel)
        {
            this.CarModel = CarModel;
            this.Year = statistics.First().Year;
            foreach (var s in statistics.Where(n => n.Year == Year))
                if(!SalesPerMonth.ContainsKey(s.Month)) SalesPerMonth.Add(s.Month, s.TotalSales);
            TotalSalesForYear = Math.Round(SalesPerMonth.Sum(v => v.Value), 2);
        }

        public string CarModel { get; set; }
        public int Year { get; private set; }
        public Dictionary<int, double> SalesPerMonth { get; set; } = new();
        public double TotalSalesForYear { get; set; }

        
    }
}
