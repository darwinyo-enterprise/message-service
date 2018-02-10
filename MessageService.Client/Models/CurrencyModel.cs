using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Models
{
    public class CurrencyModel:EventArgs
    {
        public string Name { get; set; }
        public decimal ValuePerUSD { get; set; }
    }
}
