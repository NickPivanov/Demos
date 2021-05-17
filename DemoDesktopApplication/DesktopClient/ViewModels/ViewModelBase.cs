using Domain.Models;
using System.Collections.ObjectModel;

namespace DesktopClient.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        public static ObservableCollection<Motor> MotorsCollection;
    }
}
