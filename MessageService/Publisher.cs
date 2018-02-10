using MessageService.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MessageService
{
    /// <summary>
    /// Base Abstraction Layer To Implement other Publishers
    /// </summary>
    /// <typeparam name="T">
    /// Object For Broadcast to other subscriber
    /// </typeparam>
    public class Publisher<T> : IPublisher<T> where T : class, new()
    {

        private event EventHandler<T> _eventHandler;


        /// <summary>
        /// Event To Notify Publisher has Triggered Send...
        /// </summary>
        public event EventHandler<T> eventHandler
        {
            add
            {
                // Avoid Same Delegate Registered
                if (_eventHandler == null || !_eventHandler.GetInvocationList().Contains(value))
                {
                    _eventHandler += value;
                }
            }
            remove
            {
                _eventHandler -= value;
            }
        }

        /// <summary>
        /// Publisher Broadcast...
        /// </summary>
        /// <param name="message">
        /// Object To Broadcast..
        /// </param>
        public virtual void Send(T message)
        {
            OnEventTriggered(message);
        }

        /// <summary>
        /// When Event Triggered Then Send To Subscribers...
        /// </summary>
        /// <param name="message"></param>
        protected virtual void OnEventTriggered(T message)
        {
            // If Someone Subscribe then invoke their method...
            _eventHandler?.Invoke(this, message);
        }

        #region Unit testing Utilities

        /// <summary>
        /// Helper for Testing...
        /// Used To Check Is Delegate registered in eventhandler.
        /// </summary>
        /// <param name="delegate">
        /// event delegate that want to verify
        /// </param>
        public bool IsDelegateHooked(Delegate @delegate)
        {
            return GetAllDelegateHookedToEventHandler().Where(x => x.Method.Name == @delegate.Method.Name && x.Method.DeclaringType.Namespace == @delegate.Method.DeclaringType.Namespace).First() != null ? true : false;
        }
        /// <summary>
        /// Make sure Delegate Only Hooked Once even when delegate tries to subscribe multiple times.
        /// </summary>
        /// <param name="delegate">
        /// event delegate that want to verify
        /// </param>
        /// <returns>
        /// Is Delegate That Specified Only Hooked Once...
        /// </returns>
        public bool IsDelegateHookedOnce(Delegate @delegate)
        {
            return GetAllDelegateHookedToEventHandler().Where(x => x.Method.Name == @delegate.Method.Name && x.Method.DeclaringType.Namespace == @delegate.Method.DeclaringType.Namespace).Count() == 1 ? true : false;
        }
        /// <summary>
        /// Used For Get All Delegate that hooked to event handler
        /// </summary>
        /// <returns>
        /// All Delegate That Listed In Event handler
        /// </returns>
        public Delegate[] GetAllDelegateHookedToEventHandler()
        {
            return _eventHandler.GetInvocationList();
        }
        #endregion

    }
}
