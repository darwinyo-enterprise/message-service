using MessageService.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Factory
{
    public static class CurrencyFactoryExtension
    {
        /// <summary>
        /// Ext method for initializing
        /// </summary>
        public static CurrencyModel InitialCurrency(this CurrencyModel currencyModel, string name, decimal initialPrice)
        {
            currencyModel.Name = name;
            currencyModel.ValuePerUSD = initialPrice;
            return currencyModel;
        }
    }
}
