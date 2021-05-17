using DataAccess.NetCore;
using DataAccess.NetCore.Services;
using DesktopClient.ViewModels;
using DesktopClient.Views;
using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider serviceProvider = CreateServiceProvider();

        Microsoft.Extensions.Logging.ILogger logger = serviceProvider.GetRequiredService<ILoggerProvider>()
                .CreateLogger("App");
        protected override void OnStartup(StartupEventArgs e)
        {
            logger.LogInformation("Starting application");

            var culture = new CultureInfo("en-US");

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Window window = serviceProvider.GetRequiredService<StartupView>();
            window.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            logger.LogInformation("Application stopped");
            base.OnExit(e);
        }

        private static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            //registring db context
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var optionsbuilder = new DbContextOptionsBuilder<MotorDbContext>();
            optionsbuilder.UseSqlServer(connectionString);
            services.AddSingleton(s => new MotorDbContext(optionsbuilder.Options));
            
            //registring startup view
            services.AddScoped<StartupViewModel>();
            services.AddScoped(s => new StartupView(s.GetRequiredService<StartupViewModel>()));

            //registring services
            services.AddTransient(s => new MotorService<Motor>(s.GetRequiredService<MotorDbContext>()));
            services.AddTransient<IMotorService<Motor>, MotorService<Motor>>();
            services.AddTransient(s => new MotorPropertyService<MotorProperty>(s.GetRequiredService<MotorDbContext>()));
            services.AddTransient<IMotorPropertyService<MotorProperty>, MotorPropertyService<MotorProperty>>();
            services.AddTransient(s => new MeasurementService<Measurement>(s.GetRequiredService<MotorDbContext>()));
            services.AddTransient<IMeasurementService<Measurement>, MeasurementService<Measurement>>();

            //registring other views
            services.AddSingleton(s => new MotorsViewModel(s.GetRequiredService<IMotorService<Motor>>()));
            services.AddScoped(s => new MotorsView(s.GetRequiredService<MotorsViewModel>()));
            services.AddSingleton(s => new MeasurementsViewModel(s.GetRequiredService<IMeasurementService<Measurement>>()));
            services.AddScoped(s => new MeasurementsView(s.GetRequiredService<MeasurementsViewModel>()));
            services.AddSingleton(s => new MotorFormViewModel(s.GetRequiredService<IMotorService<Motor>>(), 
                s.GetRequiredService<IMotorPropertyService<MotorProperty>>()));
            services.AddTransient(s => new MotorFormView(s.GetRequiredService<MotorFormViewModel>()));

            //logging
            services.AddLogging(configure => {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), rollingInterval: RollingInterval.Day)
                .MinimumLevel.Debug()
                .MinimumLevel.Override("DesktopClient.Commands", Serilog.Events.LogEventLevel.Debug);

                configure.ClearProviders();
                configure.AddSerilog(loggerConfiguration.CreateLogger());
            });

            return services.BuildServiceProvider();
        }
    }
}
