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
    public class CNYSubject : Subject<ICNYPublisher, CurrencyModel>, ICNYSubject
    {
        private readonly ILogger<CNYSubject> _logger;

        public CNYSubject(ILogger<CNYSubject> logger)
        {
            _logger = logger;
        }
        public override void Subscribe(ICNYPublisher publisher, ISubscriber<CurrencyModel> subscriber)
        {
            _logger.LogInformation("CNY Subscribed...");
            base.Subscribe(publisher, subscriber);
        }
        public override void Unsubscribe(ICNYPublisher publisher, ISubscriber<CurrencyModel> subscriber)
        {
            _logger.LogInformation("CNY Unsubscribed...");
            base.Unsubscribe(publisher, subscriber);
        }
    }
}
