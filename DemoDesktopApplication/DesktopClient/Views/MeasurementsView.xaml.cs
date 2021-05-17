using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for MeasurementsView.xaml
    /// </summary>
    public partial class MeasurementsView : UserControl
    {
        public MeasurementsView() { }
        public MeasurementsView(object datacontext)
        {
            DataContext = datacontext;
            InitializeComponent();
            MeasurementsGrid.Columns.Add(
              new DataGridTextColumn { Header = "Time", Binding = new Binding("Time") { StringFormat = "hh:mm:ss" } });
            MeasurementsGrid.Columns.Add(
                new DataGridTextColumn { Header = "Motor", Binding = new Binding("Motor.Name") });
            MeasurementsGrid.Columns.Add(
                new DataGridTextColumn { Header = "Property", Binding = new Binding("MotorProperty.Name") });
            MeasurementsGrid.Columns.Add(
               new DataGridTextColumn { Header = "Value", Binding = new Binding("Value") });

            FilterComboBox.SelectedIndex = 0;
        }


        private void MeasurementsGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                GridScrollViewer.LineUp();
            else if (e.Delta < 0)
                GridScrollViewer.LineDown();
        }

        
    }
}
