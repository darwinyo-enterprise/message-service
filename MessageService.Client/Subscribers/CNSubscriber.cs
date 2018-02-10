using MessageService.Client.Interfaces.Subscribers;
using MessageService.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Subscribers
{
    public class CNSubscriber : Subscriber<CurrencyModel>, ICNSubscriber
    {
        private ILogger<CNSubscriber> _logger;
        public CNSubscriber(ILogger<CNSubscriber> logger)
        {
            _logger = logger;
        }
        public override void Listen(object sender, CurrencyModel args)
        {
            _logger.LogInformation("CN Subscriber has Listen to Broadcast...");
            _logger.LogInformation("Currency Info : " + args.Name + " at " + args.ValuePerUSD);

            base.Listen(sender,args);
        }
    }
}
