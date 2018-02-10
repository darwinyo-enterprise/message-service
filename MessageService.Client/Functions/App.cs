using MessageService.Client.Consts;
using MessageService.Client.Interfaces;
using MessageService.Client.Models;
using MessageService.Client.UnitOfWork;
using MessageService.Client.Factory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using MessageService.Interfaces;
using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Publishers;

namespace MessageService.Client.Functions
{
    public class App : IApp
    {
        private ICurrencyUnitOfWork _currencyUnitOfWork;
        private ILogger<App> _logger;
        public App(ICurrencyUnitOfWork currencyUnitOfWork, ILogger<App> logger)
        {
            _currencyUnitOfWork = currencyUnitOfWork;

            // Mock Initialize
            _currencyUnitOfWork.CNYPublisher.CNY = new CurrencyModel().InitialCurrency("CNY", 6.29m);
            _currencyUnitOfWork.GBPPublisher.GBP = new CurrencyModel().InitialCurrency("GBP", 0.72m);
            _currencyUnitOfWork.JPYPublisher.JPY = new CurrencyModel().InitialCurrency("JPY", 108.61m);

            _logger = logger;
        }


        public void Appstart()
        {
            string cmd;
            // Exit App Flag
            bool exit = false;

            // Make sure User Doesnt input exit
            while (exit == false)
            {
                cmd = string.Empty;
                #region Console
                Console.WriteLine("Type EXIT to exit the application...");
                Console.WriteLine("Please Choose the actions...");
                Console.WriteLine("1. " + Actions.Subscribe);
                Console.WriteLine("2. " + Actions.Unsubscribe);
                Console.WriteLine("3. " + Actions.Broadcast);

                cmd = Console.ReadLine().ToUpper();
                #endregion

                if (cmd == Actions.Subscribe.ToUpper())
                {
                    SubscribeUnsubscribeSubject(ref cmd, true);
                }
                else if (cmd == Actions.Unsubscribe.ToUpper())
                {
                    SubscribeUnsubscribeSubject(ref cmd, false);
                }
                else if (cmd == Actions.Broadcast.ToUpper())
                {
                    DispatchAction(ref cmd);
                }

                else _logger.LogInformation("No Such Command, Please Try Again");

                OtherCommand(ref cmd, ref exit);
            }
        }

        /// <summary>
        /// Used When User Action is Broadcast...
        /// </summary>
        /// <param name="cmd">
        /// ref command
        /// </param>
        private void DispatchAction(ref string cmd)
        {
            try
            {
                #region Console
                Console.WriteLine("Please Choose the currency by Name...");
                Console.WriteLine("1. " + Currencies.GBP);
                Console.WriteLine("2. " + Currencies.JPY);
                Console.WriteLine("3. " + Currencies.CNY);

                Console.WriteLine("Please Write Currency Name...");

                cmd = Console.ReadLine().ToUpper();

                Console.WriteLine("Please Write Current Price Per USD");

                string input = Console.ReadLine();
                #endregion

                if (cmd == Currencies.GBP)
                {
                    ChangeCurrentPriceByUserInput(ref cmd, _currencyUnitOfWork.GBPPublisher, input);
                }
                else if (cmd == Currencies.JPY)
                {
                    ChangeCurrentPriceByUserInput(ref cmd, _currencyUnitOfWork.JPYPublisher, input);
                }
                else if (cmd == Currencies.CNY)
                {
                    ChangeCurrentPriceByUserInput(ref cmd, _currencyUnitOfWork.CNYPublisher, input);
                }

                else _logger.LogInformation("No Such Command, Please Try Again");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Input Must Be Decimal...");
                _logger.LogWarning(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Used in other command specified...
        /// </summary>
        /// <param name="cmd">
        /// ref command
        /// </param>
        /// <param name="exit">
        /// ref exit flag
        /// </param>
        private void OtherCommand(ref string cmd, ref bool exit)
        {
            // If CMD is EXIT then Set exit flag to true
            if (cmd == "EXIT") exit = true;
        }

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
        public void ChangeCurrentPriceByUserInput(ref string cmd, IPublisher<CurrencyModel> publisher, string input)
        {
            try
            {
                if (cmd == Currencies.GBP)
                {
                    ((IGBPPublisher)publisher).OnGBPChanged(Convert.ToDecimal(input));
                }
                else if (cmd == Currencies.JPY)
                {
                    ((IJPYPublisher)publisher).OnJPYChanged(Convert.ToDecimal(input));
                }
                else if (cmd == Currencies.CNY)
                {
                    ((ICNYPublisher)publisher).OnCNYChanged(Convert.ToDecimal(input));
                }

                else _logger.LogInformation("No Such Command, Please Try Again");
            }
            catch
            {

                throw;
            }

        }

        /// <summary>
        /// Called When Subscribe or Unsubscribe Is the action.
        /// </summary>
        /// <param name="cmd">
        /// ref command
        /// </param>
        /// <param name="subscribe">
        /// Subscribe = true
        /// Unsubscribe = false
        /// </param>
        private void SubscribeUnsubscribeSubject(ref string cmd, bool subscribe)
        {
            #region Console things
            Console.WriteLine("Please Choose the Subscriber by Name...");
            Console.WriteLine("1. " + Subs.CNSub);
            Console.WriteLine("2. " + Subs.USSub);
            Console.WriteLine("3. " + Subs.JPNSub);

            Console.WriteLine("Please Write Subscriber Name...");

            cmd = Console.ReadLine().ToUpper();
            #endregion

            // Doing To Select Target Subject
            if (cmd == Subs.CNSub.ToUpper())
            {
                SelectTargetSubjectAndDoActions(ref cmd, _currencyUnitOfWork.CNSubscriber, subscribe);
            }
            else if (cmd == Subs.USSub.ToUpper())
            {
                SelectTargetSubjectAndDoActions(ref cmd, _currencyUnitOfWork.USSubscriber, subscribe);
            }
            else if (cmd == Subs.JPNSub.ToUpper())
            {
                SelectTargetSubjectAndDoActions(ref cmd, _currencyUnitOfWork.JPNSubscriber, subscribe);
            }

            else _logger.LogInformation("No Such Command, Please Try Again");

        }

        /// <summary>
        /// Used To Select Which Subject to Subscribe, And Subscribe...
        /// </summary>
        /// <param name="cmd">
        /// ref command
        /// </param>
        /// <param name="subscriber">
        /// ref selected subscriber
        /// </param>
        /// <param name="subscribe">
        /// if true then subscribe user, if false then unsubscribe...
        /// </param>
        private void SelectTargetSubjectAndDoActions(ref string cmd, ISubscriber<CurrencyModel> subscriber, bool subscribe)
        {
            #region Console
            Console.WriteLine("Please Choose the Subject by Name...");
            Console.WriteLine("1. " + TargetSubject.CNYSubj);
            Console.WriteLine("2. " + TargetSubject.GBPSubj);
            Console.WriteLine("3. " + TargetSubject.JPYSubj);

            Console.WriteLine("Please Write Subject Name...");
            cmd = Console.ReadLine().ToUpper();
            #endregion

            // Things To Subscribe and Unsubscribe...
            SubscribeUnsubscribeByCMD(cmd, subscriber, subscribe);
        }

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
        public void SubscribeUnsubscribeByCMD(string cmd, ISubscriber<CurrencyModel> subscriber, bool subscribe)
        {
            // Doing Subscribe/ unsubscribe
            if (cmd == TargetSubject.CNYSubj.ToUpper())
            {
                // Execute Type actions by flag passed...
                if (subscribe) _currencyUnitOfWork.CNYSubject.Subscribe(_currencyUnitOfWork.CNYPublisher, subscriber);

                else _currencyUnitOfWork.CNYSubject.Unsubscribe(_currencyUnitOfWork.CNYPublisher, subscriber);
            }
            else if (cmd == TargetSubject.GBPSubj.ToUpper())
            {
                if (subscribe) _currencyUnitOfWork.GBPSubject.Subscribe(_currencyUnitOfWork.GBPPublisher, subscriber);

                else _currencyUnitOfWork.GBPSubject.Unsubscribe(_currencyUnitOfWork.GBPPublisher, subscriber);
            }
            else if (cmd == TargetSubject.JPYSubj.ToUpper())
            {
                if (subscribe) _currencyUnitOfWork.JPYSubject.Subscribe(_currencyUnitOfWork.JPYPublisher, subscriber);

                else _currencyUnitOfWork.JPYSubject.Unsubscribe(_currencyUnitOfWork.JPYPublisher, subscriber);
            }

            else _logger.LogInformation("No Such Command, Please Try Again");
        }
    }
}
