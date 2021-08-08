using DesktopClient.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for StartupView.xaml
    /// </summary>
    public partial class StartupView : Window
    {
        private static IExportService exportService;
        public StartupView(object datacontext)
        {
            InitializeComponent();
            DataContext = datacontext;
            exportService = App.ServiceProvider.GetRequiredService<IExportService>();
            //add columns to DataGrid
            StatisticDataGrid.Columns.Add(new DataGridTextColumn { Header = "Model", Binding = new Binding("CarModel") });
            StatisticDataGrid.Columns.Add(new DataGridTextColumn { Header = "Year", Binding = new Binding("Year") });
            StatisticDataGrid.Columns.Add(new DataGridTextColumn { Header = "Total", Binding = new Binding("TotalSalesForYear") });

            foreach (string month in DateTimeFormatInfo.InvariantInfo.MonthNames)
            {
                if (!string.IsNullOrEmpty(month))
                {
                    //get value from dictionary, where month name is a key
                    Binding salesBinding = new ("SalesPerMonth");
                    salesBinding.Converter = new SalesValueConverter(month);
                    //change datagrid cell style depending on cell value
                    Style DataGridCellStyle = new(typeof(DataGridCell));
                    Setter setter = new() { Property = BackgroundProperty };
                    setter.Value = new Binding() {Converter = new CellStyleValueConverter(month) };
                    DataGridCellStyle.Setters.Add(setter);
                    //add column with a month name header
                    var ValueColumn = new DataGridTextColumn { Header = month, Binding = salesBinding };
                    ValueColumn.CellStyle = DataGridCellStyle;
                    salesBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    StatisticDataGrid.Columns.Add(ValueColumn);
                }
            }

            Models_ComboBox.SelectedIndex = 0;
        }

        private void StatistiDataGrid_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                GridScrollViewer.LineUp();
            else if (e.Delta < 0)
                GridScrollViewer.LineDown();
        }

        private async void Export_Btn_Click(object sender, RoutedEventArgs e)
        {
            var statistics = StatisticDataGrid.ItemsSource as List<AnnualStatistics>;
            DataTable dt = new();
            foreach (var column in StatisticDataGrid.Columns)
            {
                dt.Columns.Add(new DataColumn {ColumnName = column.Header.ToString()});
            }

            foreach (var item in statistics)
            {
                FillDataTableRow(dt, item);
            }

            var result = await exportService.ExportAsync(dt);
            string message = result is null ? "Saved to Desktop" : result;
            MessageBox.Show(message);
        }

        //Convert DataGrid item to DataTable row and add this row to selected table
        private static void FillDataTableRow(DataTable table, AnnualStatistics statistic)
        {
            DataRow row = table.NewRow();
            row["Model"] = statistic.CarModel;
            row["Year"] = statistic.Year;
            row["Total"] = statistic.TotalSalesForYear;

            foreach (string month in DateTimeFormatInfo.InvariantInfo.MonthNames)
            {
                if (!string.IsNullOrEmpty(month))
                {
                    var DictionaryConverter = new SalesValueConverter(month);
                    var sales = DictionaryConverter.Convert(statistic.SalesPerMonth, null, null, null);
                    row[month] = sales;
                }
            }
            table.Rows.Add(row);
        }

       
    }
}
