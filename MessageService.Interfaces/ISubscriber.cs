using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Interfaces
{
    /// <summary>
    /// base subscriber that will inherited by other Subscribers...
    /// </summary>
    /// <typeparam name="T">
    /// Object To get
    /// </typeparam>
    public interface ISubscriber<T> where T : class, new()
    {
        /// <summary>
        /// Event That Triggered When Subscriber Get Observables...
        /// </summary>
        event EventHandler<T> listenEventHandler;
        /// <summary>
        /// This Will Triggered Every Time Publisher dispatch event...
        /// </summary>
        /// <param name="sender">
        /// Sender Object
        /// </param>
        /// <param name="args">
        /// Arguments passed
        /// </param>
        void Listen(object sender, T args);
    }
}
