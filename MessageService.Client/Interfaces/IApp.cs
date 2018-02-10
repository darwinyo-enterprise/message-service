using MessageService.Client.Models;
using MessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Interfaces
{
    public interface IApp
    {
        void Appstart();

        /// <summary>
        /// Made to develop unit test easier...
        /// Real Function To Subscribe And Unsubscribe...
        /// </summary>
        /// 
        /// <param name="cmd">
        /// ref command
        /// </param>
        /// <param name="subscriber">
        /// ref selected subscriber
        /// </param>
        /// <param name="subscribe">
        /// if true then subscribe user, if false then unsubscribe...
        /// </param>
        void SubscribeUnsubscribeByCMD(string cmd, ISubscriber<CurrencyModel> subscriber, bool subscribe);

        /// <summary>
        /// Used For Modify Reference Field By User Input...
        /// Optimized for Unit test...
        /// </summary>
        /// <param name="cmd">
        /// reference command string
        /// </param>
        /// <param name="currencyModel">
        /// reference Currency Model
        /// </param>
        /// <param name="input">
        /// User Input Decimal
        /// </param>
        void ChangeCurrentPriceByUserInput(ref string cmd, IPublisher<CurrencyModel> publisher, string input);
    }
}
