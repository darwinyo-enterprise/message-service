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
    public class GBPSubject : Subject<IGBPPublisher, CurrencyModel>, IGBPSubject
    {
        private readonly ILogger<GBPSubject> _logger;

        public GBPSubject(ILogger<GBPSubject> logger)
        {
            _logger = logger;
        }
        public override void Subscribe(IGBPPublisher publisher, ISubscriber<CurrencyModel> subscriber)
        {
            _logger.LogInformation("GBP Subscribed...");
            base.Subscribe(publisher, subscriber);
        }
        public override void Unsubscribe(IGBPPublisher publisher, ISubscriber<CurrencyModel> subscriber)
        {
            _logger.LogInformation("GBP Unsubscribed...");
            base.Unsubscribe(publisher, subscriber);
        }
    }
}
