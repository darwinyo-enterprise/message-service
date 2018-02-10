using MessageService.Client.Models;
using MessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Interfaces.Publishers
{
    public interface ICNYPublisher : IPublisher<CurrencyModel>
    {
        CurrencyModel CNY { get; set; }
        /// <summary>
        /// When CNY Changed This Method Called...
        /// </summary>
        /// <param name="currentPrice">
        /// Current Currency Price
        /// </param>
        void OnCNYChanged(decimal currentPrice);
    }
}
