using MessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MessageService
{
    /// <summary>
    /// base subject to implement to others subjects...
    /// </summary>
    /// <typeparam name="TPublisher">
    /// publisher to subscribe...
    /// </typeparam>
    /// <typeparam name="T">
    /// Object to Subscribe
    /// </typeparam>
    public class Subject<TPublisher, T> : ISubject<TPublisher, T>
        where T : class, new()
        where TPublisher : IPublisher<T>
    {
        public Subject()
        {
        }

        /// <summary>
        /// Add Subscriber To Subject.
        /// </summary>
        /// <param name="subscriber">
        /// Subscriber To Add.
        /// </param>
        /// <returns></returns>
        public virtual void Subscribe(TPublisher publisher, ISubscriber<T> subscriber)
        {
            try
            {
                // Subscribe Subscriber To Publisher...
                publisher.eventHandler += subscriber.Listen;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Remove Subscriber From Subscribe.
        /// </summary>
        /// <param name="subscriber">
        /// Subscriber To Remove.
        /// </param>
        public virtual void Unsubscribe(TPublisher publisher, ISubscriber<T> subscriber)
        {
            try
            {
                // unsubcribes
                publisher.eventHandler -= subscriber.Listen;
            }
            catch
            {

                throw;
            }
        }
    }
}
