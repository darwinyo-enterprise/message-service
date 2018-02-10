using MessageService.Client.Consts;
using MessageService.Client.Functions;
using MessageService.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using MessageService.Client.Models;
using MessageService.Client.Interfaces.Subscribers;
using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Interfaces.Subjects;
using MessageService.Client.Subscribers;
using MessageService.Client.UnitOfWork;

namespace MessageService.Client.Tests
{
    public class Tests
    {
        private ServiceProvider provider;

        private ICurrencyUnitOfWork _currencyUnitOfWork;
        
        private IApp _app;

        delegate void ListenDelegate(object sender, CurrencyModel args);

        /// <summary>
        /// Test Cases :
        /// 1. Make Sure Subscriber Are able to subscribe correctly...
        /// 2. Make Sure When Publisher Dispatch Observables, Those Observables is Only Listened By Who Subscribe That Observables...
        /// 3. Make Sure When Subscriber Unsubscribe the Subject, THose Subscriber wont listen to publisher
        /// 4. Make Sure If User Typo In Setting Current price with non numberic throw error.
        /// </summary>
        public Tests()
        {
            #region Services
            var serviceCollection = new ServiceCollection();

            Startup.ConfigureServices(serviceCollection);

            provider = serviceCollection.BuildServiceProvider();
            #endregion

            _currencyUnitOfWork = new CurrencyUnitOfWork();
            _currencyUnitOfWork.ServiceProvider = provider;

            _app = new App(_currencyUnitOfWork, null);
        }
        /// <summary>
        /// Validate Test cases for Delegates Registers
        /// </summary>
        #region Subscriber Make Subscription Validate Delegates are registered
        [Fact]
        public void ShouldHookedToEventHandler_WhenSubscriberSubscribeToSubject()
        {
            var del = new ListenDelegate(_currencyUnitOfWork.JPNSubscriber.Listen); // Delegate of JPN Subscriber

            _app.SubscribeUnsubscribeByCMD(TargetSubject.CNYSubj.ToUpper(), _currencyUnitOfWork.JPNSubscriber, true); // JPN Subscriber Subscribe To CNY Subject.

            Assert.True(_currencyUnitOfWork.CNYPublisher.IsDelegateHooked(del)); // Assert That delegate is hooked...
        }

        [Fact]
        public void ShouldHookedOnceToEventHandler_EvenWhenSubscriberTriesToSubscribeMoreThanOnce()
        {
            var del = new ListenDelegate(_currencyUnitOfWork.JPNSubscriber.Listen); // Delegate of JPN Subscriber

            _app.SubscribeUnsubscribeByCMD(TargetSubject.CNYSubj.ToUpper(), _currencyUnitOfWork.JPNSubscriber, true); // JPN Subscriber Subscribe To CNY Subject.

            // Subscribe twice
            _app.SubscribeUnsubscribeByCMD(TargetSubject.CNYSubj.ToUpper(), _currencyUnitOfWork.JPNSubscriber, true); // JPN Subscriber Subscribe To CNY Subject.

            Assert.True(_currencyUnitOfWork.CNYPublisher.IsDelegateHookedOnce(del)); // Assert That delegate is hooked only once...
        }
        #endregion

        #region Publisher Emit Observable than Subscriber Should Listen To them
        [Fact]
        public void ShouldListenToObservablesWhereOnlySubscriberWhoSubscribe_WhenPublisherDispatchObservable()
        {
            // Simulate 2 Subscriber To Subscribe one subject, Other one doesnt...
            // US Subscriber Doesnt subscribe...
            // subscribe true means to subscribe.
            // subscribe false means to unsubscribe.
            _app.SubscribeUnsubscribeByCMD(TargetSubject.CNYSubj.ToUpper(), _currencyUnitOfWork.JPNSubscriber, true); // JPN Subscriber Subscribe to CNY Subject.
            _app.SubscribeUnsubscribeByCMD(TargetSubject.CNYSubj.ToUpper(), _currencyUnitOfWork.CNSubscriber, true); // CN subscriber subscribe to CNY Subject.

            #region Verify Listeners
            bool cnListen = false;
            bool usListen = false;
            bool jpnListen = false;

            // Assert that Publisher Dispatch Event...
            // Subscribe to ListenEventHandlers...
            _currencyUnitOfWork.CNSubscriber.listenEventHandler += delegate (object sender, CurrencyModel currencyModel)
            {
                cnListen = true;
            };
            _currencyUnitOfWork.USSubscriber.listenEventHandler += delegate (object sender, CurrencyModel currencyModel)
            {
                usListen = true;
            };
            _currencyUnitOfWork.JPNSubscriber.listenEventHandler += delegate (object sender, CurrencyModel currencyModel)
            {
                jpnListen = true;
            };
            #endregion

            string broadcast = Currencies.CNY;
            // Executing Broadcast
            _app.ChangeCurrentPriceByUserInput(ref broadcast, _currencyUnitOfWork.CNYPublisher, "123"); // Broadcast CNY and changed value to 123, 

            Assert.True(cnListen); // Assert that CN Subscriber get the broadcast
            Assert.True(jpnListen); // Assert that JPN Subscriber get the broadcast
            Assert.False(usListen); // Assert that US Subscriber doesnt get the broadcast.

        }

        [Fact]
        public void ShouldNotListenToObservableAfterUnsubscribe_WhenPublisherDispatchObservable()
        {
            // Simulate 2 Subscriber To Subscribe one subject, Other one doesnt...
            // US Subscriber Doesnt subscribe...
            // subscribe true means to subscribe.
            // subscribe false means to unsubscribe.
            _app.SubscribeUnsubscribeByCMD(TargetSubject.CNYSubj.ToUpper(), _currencyUnitOfWork.JPNSubscriber, true); // JPN Subscriber Subscribe to CNY Subject.
            _app.SubscribeUnsubscribeByCMD(TargetSubject.CNYSubj.ToUpper(), _currencyUnitOfWork.CNSubscriber, true); // CN subscriber subscribe to CNY Subject.

            #region Verify Listeners
            bool cnListen = false;
            bool usListen = false;
            bool jpnListen = false;

            // Assert that Publisher Dispatch Event...
            // Subscribe to ListenEventHandlers...
            _currencyUnitOfWork.CNSubscriber.listenEventHandler += delegate (object sender, CurrencyModel currencyModel)
            {
                cnListen = true;
            };
            _currencyUnitOfWork.USSubscriber.listenEventHandler += delegate (object sender, CurrencyModel currencyModel)
            {
                usListen = true;
            };
            _currencyUnitOfWork.JPNSubscriber.listenEventHandler += delegate (object sender, CurrencyModel currencyModel)
            {
                jpnListen = true;
            };
            #endregion

            string broadcast = Currencies.CNY;
            // Executing Broadcast
            _app.ChangeCurrentPriceByUserInput(ref broadcast, _currencyUnitOfWork.CNYPublisher, "123"); // Broadcast CNY and changed value to 123, 

            Assert.True(cnListen); // Assert that CN Subscriber get the broadcast
            Assert.True(jpnListen); // Assert that JPN Subscriber get the broadcast
            Assert.False(usListen); // Assert that US Subscriber doesnt get the broadcast.

            // Clear State
            cnListen = false;
            usListen = false;
            jpnListen = false;

            _app.SubscribeUnsubscribeByCMD(TargetSubject.CNYSubj.ToUpper(), _currencyUnitOfWork.CNSubscriber, false); // Unsubscribe CN Subcriber

            // Executing Broadcast
            _app.ChangeCurrentPriceByUserInput(ref broadcast, _currencyUnitOfWork.CNYPublisher, "121"); // Broadcast CNY and changed value to 121, 

            Assert.False(cnListen); // Assert that CN Subscriber doesnt get the broadcast
            Assert.True(jpnListen); // Assert that JPN Subscriber get the broadcast
        }
        #endregion

        [Fact]
        public void ShouldThrowError_WhenUserTypoInDecimalInput()
        {
            var cmd = Currencies.GBP;

            Assert.Throws<FormatException>(() => _app.ChangeCurrentPriceByUserInput(ref cmd, _currencyUnitOfWork.GBPPublisher, "abcd"));
        }
    }
}
