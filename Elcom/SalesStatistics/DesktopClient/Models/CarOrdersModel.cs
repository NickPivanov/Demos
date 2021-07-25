
namespace DesktopClient.Models
{
    //Intermediate model to convert database models to UI model
    public class CarOrdersModel
    {
        public int CarId { get; set; }
        public string CarModel { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double TotalSales { get; set; }
    }
}
