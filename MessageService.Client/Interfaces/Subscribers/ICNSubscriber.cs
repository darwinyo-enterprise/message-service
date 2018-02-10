using MessageService.Client.Models;
using MessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Interfaces.Subscribers
{
    public interface ICNSubscriber : ISubscriber<CurrencyModel>
    {
    }
}
