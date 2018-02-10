using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Publishers
{
    public class CNYPublisher : Publisher<CurrencyModel>,ICNYPublisher
    {
        private readonly ILogger<CNYPublisher> _logger;
        public CurrencyModel CNY { get; set; }
        public CNYPublisher(ILogger<CNYPublisher> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// When CNY Changed This Method Called...
        /// </summary>
        /// <param name="currencyModel">
        /// Current Currency Price
        /// </param>
        public void OnCNYChanged(decimal currentPrice)
        {
            CNY.ValuePerUSD = currentPrice;
            Send(CNY);
        }
    }
}
