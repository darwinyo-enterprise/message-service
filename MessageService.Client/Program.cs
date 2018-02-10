using MessageService.Client.Functions;
using MessageService.Client.Interfaces;
using MessageService.Client.Publishers;
using MessageService.Client.Subjects;
using MessageService.Client.UnitOfWork;
using MessageService.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MessageService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Application Database Utility Starting...");

                var serviceCollection = new ServiceCollection();

                Startup.ConfigureServices(serviceCollection);

                var provider = serviceCollection.BuildServiceProvider();

                var loggerFactory = provider
                    .GetService<ILoggerFactory>()
                    .AddConsole(LogLevel.Debug);

                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogInformation("All Application Configuration Completed...");
                logger.LogInformation("Application Started...");

                // App Start
                var unitOfWork = new CurrencyUnitOfWork();
                unitOfWork.ServiceProvider = provider;

                var app = new App(unitOfWork, loggerFactory.CreateLogger<App>());
                app.Appstart();

                logger.LogInformation("All Execution Completed. Press Any Key To Exit...");
                Console.ReadLine();

            }
            catch
            {
                throw;
            }
        }


    }
}
