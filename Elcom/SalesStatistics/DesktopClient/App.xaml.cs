using DataAccess;
using DataAccess.Services;
using DesktopClient.Models;
using DesktopClient.Services;
using DesktopClient.ViewModels;
using DesktopClient.Views;
using Domain;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Windows;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
            ServiceProvider = _host.Services;
            logger =
            ServiceProvider.GetRequiredService<ILoggerProvider>().CreateLogger("App");
        }
        
        public static IServiceProvider ServiceProvider { get; private set; }

        Microsoft.Extensions.Logging.ILogger logger;
        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (config) =>
                {
                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    //configuring DB access
                    services.AddSingleton(new SalesDbContextFactory(context.Configuration.GetConnectionString("Default")));
                    services.AddDbContext<SalesDbContext>(o => o.UseSqlite(context.Configuration.GetConnectionString("Default"))); //required for migrations
                    
                    //registering services for data operations
                    services.AddTransient(s => new CarService<Car>(s.GetRequiredService<SalesDbContext>()));
                    services.AddTransient(s => new OrderService<Order>(s.GetRequiredService<SalesDbContext>()));
                    services.AddTransient<ISalesService<Car>, CarService<Car>>();
                    services.AddTransient<ISalesService<Order>, OrderService<Order>>();
                    services.AddTransient<StatisticsService>();

                    //registering views
                    services.AddScoped(s => new StartupViewModel(s.GetRequiredService<StatisticsService>()));
                    services.AddScoped(s => new StartupView(s.GetRequiredService<StartupViewModel>()));

                    //other services
                    services.AddSingleton<IExportService, ExcelExportService>();

                    //logging
                    services.AddLogging(configure => {
                        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                        .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), rollingInterval: RollingInterval.Day)
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("DesktopClient.Services", Serilog.Events.LogEventLevel.Debug);

                        configure.ClearProviders();
                        configure.AddSerilog(loggerConfiguration.CreateLogger());
                    });
                });
        }

        
        protected override void OnStartup(StartupEventArgs e)
        {
            logger.LogInformation("Starting application");
            _host.Start();
            Window window = _host.Services.GetRequiredService<StartupView>();
            window.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            logger.LogInformation("Application stopped");
            _host.Dispose();
            base.OnExit(e);
        }

        
    }
}
