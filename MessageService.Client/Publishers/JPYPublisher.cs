using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Publishers
{
    public class JPYPublisher : Publisher<CurrencyModel>, IJPYPublisher
    {
        private readonly ILogger<JPYPublisher> _logger;
        public CurrencyModel JPY { get; set; }

        public JPYPublisher(ILogger<JPYPublisher> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// When JPY Changed This Method Called...
        /// </summary>
        /// <param name="currencyModel">
        /// Current Currency Price
        /// </param>
        public void OnJPYChanged(decimal currentPrice)
        {
            JPY.ValuePerUSD = currentPrice;
            Send(JPY);
        }
    }
}
