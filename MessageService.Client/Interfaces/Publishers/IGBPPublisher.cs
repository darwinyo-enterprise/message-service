using MessageService.Client.Models;
using MessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Interfaces.Publishers
{
    public interface IGBPPublisher : IPublisher<CurrencyModel>
    {
        CurrencyModel GBP { get; set; }
        /// <summary>
        /// When GBP Changed This Method Called...
        /// </summary>
        /// <param name="currentPrice">
        /// Current Currency Price
        /// </param>
        void OnGBPChanged(decimal currentPrice);

    }
}
