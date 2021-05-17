using Domain.Models;
using System;
using System.Linq;
using System.Windows;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for MotorFormView.xaml
    /// </summary>
    public partial class MotorFormView : Window
    {
        public MotorFormView() { }
        public MotorFormView(object datacontext)
        {
            InitializeComponent();
            DataContext = datacontext;
            MainGrid.MouseDown += (s, e) => this.DragMove();
            Cancel_btn.Click += (s, e) => this.Close();
            MotorTypeComboBox.Loaded += (s, e) =>
            {
                MotorTypeComboBox.ItemsSource = Enum.GetNames(typeof(MotorType)).ToList();
                MotorTypeComboBox.SelectedItem = MotorTypeComboBox.Items[0];
            };
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
