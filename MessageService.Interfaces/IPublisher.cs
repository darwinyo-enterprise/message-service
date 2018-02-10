using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Interfaces
{
    /// <summary>
    /// Base Abstraction Layer To Implement other Publishers
    /// </summary>
    /// <typeparam name="T">
    /// Object For Broadcast to other subscriber
    /// </typeparam>
    public interface IPublisher<T> where T : class, new()
    {

        /// <summary>
        /// Event To Notify Publisher has Triggered Send...
        /// </summary>
        event EventHandler<T> eventHandler;
        /// <summary>
        /// Publisher Broadcast...
        /// </summary>
        /// <param name="message">
        /// Object To Broadcast..
        /// </param>
        void Send(T message);

        #region Unit testing Utilities
        /// <summary>
        /// Helper for Testing...
        /// Used To Check Is Delegate registered in eventhandler.
        /// </summary>
        /// <param name="delegate">
        /// event delegate that want to verify
        /// </param>
        bool IsDelegateHooked(Delegate @delegate);

        /// <summary>
        /// Make sure Delegate Only Hooked Once even when delegate tries to subscribe multiple times.
        /// </summary>
        /// <param name="delegate">
        /// event delegate that want to verify
        /// </param>
        /// <returns>
        /// Is Delegate That Specified Only Hooked Once...
        /// </returns>
        bool IsDelegateHookedOnce(Delegate @delegate);

        /// <summary>
        /// Used For Get All Delegate that hooked to event handler
        /// </summary>
        /// <returns>
        /// All Delegate That Listed In Event handler
        /// </returns>
        Delegate[] GetAllDelegateHookedToEventHandler();
        #endregion
    }
}
