using MessageService.Client.Functions;
using MessageService.Client.Interfaces;
using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Interfaces.Subjects;
using MessageService.Client.Interfaces.Subscribers;
using MessageService.Client.Models;
using MessageService.Client.Publishers;
using MessageService.Client.Subjects;
using MessageService.Client.Subscribers;
using MessageService.Client.UnitOfWork;
using MessageService.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            Console.WriteLine("Configuring Dependecies Injection...");
            //setup our DI
            serviceCollection
                .AddLogging()
                .AddScoped<ICNYPublisher, CNYPublisher>()
                .AddScoped<IJPYPublisher, JPYPublisher>()
                .AddScoped<IGBPPublisher, GBPPublisher>()

                .AddScoped<ICNSubscriber, CNSubscriber>()
                .AddScoped<IJPNSubscriber, JPNSubscriber>()
                .AddScoped<IUSSubscriber, USSubscriber>()


                .AddScoped<ICNYSubject, CNYSubject>()
                .AddScoped<IJPYSubject, JPYSubject>()
                .AddScoped<IGBPSubject, GBPSubject>();

            Console.WriteLine("Configure Dependecies Injection Completed");
        }
    }
}
