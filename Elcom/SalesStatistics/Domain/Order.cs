using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
