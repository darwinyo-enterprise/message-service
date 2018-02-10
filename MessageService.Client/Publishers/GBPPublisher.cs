using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Publishers
{
    public class GBPPublisher : Publisher<CurrencyModel>, IGBPPublisher
    {
        private readonly ILogger<GBPPublisher> _logger;
        public CurrencyModel GBP { get; set; }
        public GBPPublisher(ILogger<GBPPublisher> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// When GBP Changed This Method Called...
        /// </summary>
        /// <param name="currencyModel">
        /// Current Currency Price
        /// </param>
        public void OnGBPChanged(decimal currentPrice)
        {
            GBP.ValuePerUSD = currentPrice;
            Send(GBP);
        }
    }
}
