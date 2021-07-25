using DesktopClient.Models;
using Domain;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopClient.Services
{
    //Service to convert database entities to models for displaying data in UI
    public class StatisticsService
    {
        private static List<Order> _orders;
        private readonly ILogger logger;

        public StatisticsService(ISalesService<Order> orderService)
        {
            _orders = orderService.GetAllAsync().GetAwaiter().GetResult();
            logger = App.ServiceProvider.GetRequiredService<ILoggerProvider>().CreateLogger("StatisticsService");
        }

        //Adopting DB entities to intermediate model CarOrdersModel
        private static List<CarOrdersModel> GetOrdersStatistics()
        {
            var statRecords = (from n in _orders
                           select new CarOrdersModel
                              {
                                CarId = n.CarId,
                                CarModel = $"{n.Car.Brand} {n.Car.Model}",
                                Month = n.OrderDate.Month,
                                Year = n.OrderDate.Year,
                                TotalSales = 0
                              }).ToList();

            foreach (var r in statRecords)
                r.TotalSales = CalculateTotalMonthSalesForCarOrder(r);

            return statRecords;
        }

        private static double CalculateTotalMonthSalesForCarOrder(CarOrdersModel orderStatisticsRecord)
        {
            var monthtotalSales = _orders.Where(c => c.CarId == orderStatisticsRecord.CarId)
                .Where(o => o.OrderDate.Year== orderStatisticsRecord.Year) 
            .Where(o => o.OrderDate.Month == orderStatisticsRecord.Month)
            .Sum(s => s.Price);

            return Math.Round(monthtotalSales, 2);
        }

        /// <summary>
        /// Get sales statistics from database.
        /// This data is supposed to be used as binding data for UI.
        /// </summary>
        /// <returns>List of AnnualStatistic models</returns>
        public List<AnnualStatistics> GetStatistics()
        {

            List<AnnualStatistics> statistics = new();
            try
            {
                var ordersStatistics = GetOrdersStatistics();
                foreach (var model in ordersStatistics.Select(m => m.CarModel).Distinct())
                {
                    foreach (int year in ordersStatistics.Select(m => m.Year).Distinct())
                        statistics.Add(new AnnualStatistics(ordersStatistics
                            .Where(m => m.CarModel == model & m.Year == year)
                            .Cast<CarOrdersModel>().ToList(), model));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting sales statistics");
            }

            return statistics;
        }
    }
}
