using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Interfaces.Subjects;
using MessageService.Client.Models;
using MessageService.Client.Publishers;
using MessageService.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Subjects
{
    public class JPYSubject : Subject<IJPYPublisher, CurrencyModel>, IJPYSubject
    {
        private readonly ILogger<JPYSubject> _logger;

        public JPYSubject(ILogger<JPYSubject> logger)
        {
            _logger = logger;
        }
        public override void Subscribe(IJPYPublisher publisher, ISubscriber<CurrencyModel> subscriber)
        {
            _logger.LogInformation("JPY Subscribed...");
            base.Subscribe(publisher, subscriber);
        }
        public override void Unsubscribe(IJPYPublisher publisher, ISubscriber<CurrencyModel> subscriber)
        {
            _logger.LogInformation("JPY Unsubscribed...");
            base.Unsubscribe(publisher, subscriber);
        }
    }
}
