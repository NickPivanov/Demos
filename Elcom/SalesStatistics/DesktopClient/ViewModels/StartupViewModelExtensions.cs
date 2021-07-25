using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    public static class StartupViewModelExtensions
    {
        internal static void FilterAnnualStatistics(this StartupViewModel vm)
        {
            vm.AnnualStatistics = vm.isEntirePeriod ? vm.sourceStatistic
                .Where(m => m.CarModel.Contains(vm.model))
                .ToList() : 
                vm.sourceStatistic.Where(m => m.CarModel.Contains(vm.model) & m.Year == vm.currentPage).ToList();

            vm.Pages = vm.AnnualStatistics.Select(n => n.Year).Distinct().Count();
            
            vm.CurrentPage = vm.AnnualStatistics.Select(n => n.Year)
                .Distinct()
                .OrderByDescending(n => n).FirstOrDefault();
            
        }

        internal static void GetPage(this StartupViewModel vm)
        {
            vm.AnnualStatistics = vm.isEntirePeriod ? vm.sourceStatistic
                .Where(m => m.CarModel.Contains(vm.model))
                .ToList() : 
                vm.sourceStatistic.Where(m => m.CarModel.Contains(vm.model) & m.Year == vm.currentPage).ToList();
        }
    }
}
