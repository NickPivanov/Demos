using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Data.ViewModels
{
    public class FilterViewModel
    {
        public string SelectedValue { get; set; }
        public FilterViewModel(string selectedValue)
        {
            SelectedValue = selectedValue;
        }
    }
}
