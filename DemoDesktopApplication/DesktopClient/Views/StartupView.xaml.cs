using DesktopClient.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for StartupView.xaml
    /// </summary>
    public partial class StartupView : Window
    {
        public StartupView(object datacontext)
        {
            InitializeComponent();
            DataContext = datacontext;
        }

        private void OptionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = OptionsListView.SelectedIndex;
            switch(index)
            {
                case 0: WorkSpaceGrid.Children.Clear();
                    WorkSpaceGrid.Children
                        .Add(App.serviceProvider.GetRequiredService<MotorsView>());
                    break;

                case 2:
                    WorkSpaceGrid.Children.Clear();
                    WorkSpaceGrid.Children
                        .Add(App.serviceProvider.GetRequiredService<MeasurementsView>());
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OptionsListView.SelectedItem = OptionsListView.Items[0];
        }
    }
}
