using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Car
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Brand { get; set; }
        [Required, MaxLength(30)]
        public string Model { get; set; }
        [Required, MaxLength(30)]
        public string ModelSet { get; set; }
        [Required, MaxLength(20)]
        public string Color { get; set; }

        public List<Order> CarOrders { get; set; }
    }
}
