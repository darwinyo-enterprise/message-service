using MessageService.Client.Interfaces.Subscribers;
using MessageService.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Subscribers
{
    public class USSubscriber : Subscriber<CurrencyModel>, IUSSubscriber
    {
        private ILogger<USSubscriber> _logger;
        public USSubscriber(ILogger<USSubscriber> logger)
        {
            _logger = logger;
        }
        public override void Listen(object sender, CurrencyModel args)
        {
            _logger.LogInformation("US Subscriber has Listen to Broadcast...");
            _logger.LogInformation("Currency Info : " + args.Name + " at " + args.ValuePerUSD);

            base.Listen(sender, args);
        }
    }
}
