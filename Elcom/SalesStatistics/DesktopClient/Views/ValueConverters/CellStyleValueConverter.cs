using DesktopClient.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DesktopClient.Views
{
    public partial class StartupView
    {
        //Changes the background color of datagrid cell is cell value is > 25
        private class CellStyleValueConverter : IValueConverter
        {
            private readonly string month;
            public CellStyleValueConverter(string Month)
            {
                month = Month;
            }
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var statistic = value as AnnualStatistics;
                int key = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
                if (statistic.SalesPerMonth.ContainsKey(key) && statistic.SalesPerMonth[key] > 25)
                {
                    return Brushes.LightSeaGreen;
                }
               
                return Brushes.Transparent;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

       
    }
}
