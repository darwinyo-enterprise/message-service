using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Interfaces
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
    public interface ISubject<TPublisher, T>
        where T : class, new()
        where TPublisher : IPublisher<T>
    {
        /// <summary>
        /// Add Subscriber To Subject.
        /// </summary>
        /// <param name="subscriber">
        /// Subscriber To Add.
        /// </param>
        /// <param name="publisher">
        /// publisher reference
        /// </param>
        /// <returns></returns>
        void Subscribe(TPublisher publisher, ISubscriber<T> subscriber);

        /// <summary>
        /// Remove Subscriber From Subscribe.
        /// </summary>
        /// <param name="subscriber">
        /// Subscriber To Remove.
        /// </param>
        /// <param name="publisher">
        /// publisher reference
        /// </param>
        void Unsubscribe(TPublisher publisher, ISubscriber<T> subscriber);
    }
}
