using MessageService.Client.Models;
using MessageService.Client.Publishers;
using MessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Interfaces.Publishers
{
    public interface IJPYPublisher : IPublisher<CurrencyModel>
    {
        CurrencyModel JPY { get; set; }
        /// <summary>
        /// When JPY Changed This Method Called...
        /// </summary>
        /// <param name="currentPrice">
        /// Current Currency Price
        /// </param>
        void OnJPYChanged(decimal currentPrice);
    }
}
