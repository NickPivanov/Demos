using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public static class SalesDbContextExtension
    {
        public static void Initialize(this SalesDbContext db)
        {
            if (db.Set<Car>().Any())
                return;
            db.Set<Car>().Add(
                new Car
                {
                    Brand = "BMW",
                    Model = "3",
                    ModelSet = "Base",
                    Color = "Red"
                });
            db.Set<Car>().Add(
                new Car
                {
                    Brand = "Audi",
                    Model = "A4",
                    ModelSet = "Advance",
                    Color = "Blue"
                });
            db.Set<Car>().Add(
                new Car
                {
                    Brand = "Lexus",
                    Model = "ES",
                    ModelSet = "Comfort",
                    Color = "White"
                });

            db.Set<Car>().Add(
                new Car
                {
                    Brand = "Hyundai",
                    Model = "Tucson",
                    ModelSet = "Classic",
                    Color = "Black"
                });
            db.Set<Car>().Add(
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Tiguan",
                    ModelSet = "Respect",
                    Color = "Gray"
                });
            db.Set<Car>().Add(
                new Car
                {
                    Brand = "Toyota",
                    Model = "Rav4",
                    ModelSet = "Standart",
                    Color = "Brown"
                });
            db.SaveChanges();

            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            using (StreamReader sr = new StreamReader("cars.json"))
                   JsonConvert.SerializeObject(db.Set<Car>().ToArray(), settings);

            foreach (var item in InitializeOrdersData(db.Set<Car>().ToList()))
            {
                db.Set<Order>().Add(item);
                db.SaveChanges();
            }

            using (StreamReader sr = new StreamReader("orders.json"))
                JsonConvert.SerializeObject(db.Set<Order>().ToArray(), settings);
        }

        private static List<Order> InitializeOrdersData(IList<Car> cars)
        {
            List<Order> orders = new List<Order>();
            Random months = new Random();
            Random days = new Random();
            foreach (var car in cars)
            {
                double price = car.Brand switch
                {
                    "BMW" => 2.91,
                    "Audi" => 2.89,
                    "Lexus" => 3.3,
                    "Volkswagen" => 2,
                    "Hyundai" => 1.9,
                    "Toyota" => 2.1,
                    _ => 0
                };

                for (int y = 0; y < 5; y++)
                {
                    if(y > 0)
                        price = Math.Round(price - (price * 0.12), 2);

                    for (int i = 0; i < 100; i++)
                    {
                        DateTime date = new DateTime(2021, 08, 01);
                        date = date.AddYears(-y).AddMonths(-months.Next(0, 12)).AddDays(days.Next(1, 28));
                        Order order = new Order()
                        {
                            Car = car,
                            Price = price,
                            CarId = car.Id,
                            OrderDate = date
                        };
                        orders.Add(order);
                    }
                }
            }
            return orders; //orders will contain 3000 records
        }
    }
}
