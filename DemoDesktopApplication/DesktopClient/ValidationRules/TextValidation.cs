using System;
using System.Globalization;
using System.Windows.Controls;

namespace DesktopClient.ValidationRules
{
    public class TextValidation : ValidationRule
    {
        public enum dataType
        {
            String, Integer, Double
        }

        public Type typeIndex { set; get; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputString = (value ?? string.Empty).ToString();
            string error = "";
            bool result = false;

            switch (typeIndex.Name)
            {
                case "Nullable":
                    result = !String.IsNullOrEmpty(inputString);
                    error = "Value shouldn't be empty";
                    break;
                case "Int32":
                    result = int.TryParse(inputString, out _);
                    error = "Value should be of type integer";
                    break;
                case "Double":
                    result = double.TryParse(inputString, out _);
                    error = "Value should be of type double";
                    break;
                
            }

            if (result == false)
            {
                return new ValidationResult(false, error);
            }

            return new ValidationResult(true, null);
        }
    }
}
