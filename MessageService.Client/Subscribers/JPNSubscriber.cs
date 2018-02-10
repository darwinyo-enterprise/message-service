using MessageService.Client.Interfaces.Subscribers;
using MessageService.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Subscribers
{
    public class JPNSubscriber : Subscriber<CurrencyModel>, IJPNSubscriber
    {
        private ILogger<JPNSubscriber> _logger;
        public JPNSubscriber(ILogger<JPNSubscriber> logger)
        {
            _logger = logger;
        }
        public override void Listen(object sender, CurrencyModel args)
        {
            _logger.LogInformation("JPN Subscriber has Listen to Broadcast...");
            _logger.LogInformation("Currency Info : " + args.Name + " at " + args.ValuePerUSD);

            base.Listen(sender, args);
        }
    }
}
