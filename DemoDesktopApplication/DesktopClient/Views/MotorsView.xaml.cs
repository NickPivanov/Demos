using DesktopClient.ValidationRules;
using Domain.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for MotorsView.xaml
    /// </summary>
    public partial class MotorsView : UserControl
    {
        public MotorsView() { }
        public MotorsView(object datacontext)
        {
            InitializeComponent();
            DataContext = datacontext;
        }

        private void MotorsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MotorsListView.SelectedItem is null)
            {
                MotorCharacteristicsPanel.Visibility = Visibility.Hidden;
                return;
            }

            MotorCharacteristicsPanel.Visibility = Visibility.Visible;
            MotorPropertiesPanel.Children.Clear();
            MotorCharacteristicsCard.DataContext = MotorsListView.SelectedItem;
            
            foreach (var motor_property in (MotorsListView.SelectedItem as Motor).Characteristics)
            {
                AddSelectedItemCharacteristicsToUI(motor_property);
            }
        }

        private void AddSelectedItemCharacteristicsToUI(Characteristic motor_property)
        {
            var propertiesPanel = new StackPanel { Orientation = Orientation.Horizontal };

            TextBox textBox = new TextBox
            {
                DataContext = motor_property,
                Style = (Style)FindResource("TextBoxStyle")
            };
            textBox.SetBinding(TextBox.TextProperty, "Value");
            SetValidationRuleForTextBox(textBox, motor_property.Value.GetType());

            propertiesPanel.Children.Add(
                new TextBlock
                {
                    Text = motor_property.MotorProperty?.Name,
                    Width = 200,
                    Style = (Style)FindResource("TxtBlockStyle")
                });
            propertiesPanel.Children.Add(textBox);

            MotorPropertiesPanel.Children.Add(propertiesPanel);
        }

        private void SetValidationRuleForTextBox(TextBox txtbox, Type valuetype)
        {
            Binding binding = BindingOperations.GetBinding(txtbox, TextBox.TextProperty);
            binding.ValidationRules.Clear();
            binding.ValidationRules.Add(new TextValidation { typeIndex = valuetype });
        }
    }
}
