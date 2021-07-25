using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DesktopClient.Views
{
    public partial class StartupView
    {
        //Column with a month name in a header suposed to be binded to dictionary<month,sales>
        //This Converter gets the value from dictionary, where month is a key
        private class SalesValueConverter : IValueConverter
        {
            private readonly string month;
            public SalesValueConverter(string Month)
            {
                month = Month;
            }

            public Style DataGridCellStyle { get; set; }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
               
                 int key = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
                 double sales = 0;
                 (value as Dictionary<int, double>).TryGetValue(key, out sales);
                return sales == 0 ? "" : sales;
                
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

       
    }
}
