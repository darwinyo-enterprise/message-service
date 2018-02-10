using MessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MessageService
{
    /// <summary>
    /// base subscriber that will inherited by other Subscribers...
    /// </summary>
    /// <typeparam name="T">
    /// Object To get
    /// </typeparam>
    public abstract class Subscriber<T> : ISubscriber<T> where T : class, new()
    {
        private event EventHandler<T> _listenEventHandler;

        /// <summary>
        /// Event To Notify Publisher has Triggered Send...
        /// </summary>
        public event EventHandler<T> listenEventHandler
        {
            add
            {
                // Avoid Same Delegate Registered
                if (_listenEventHandler == null || !_listenEventHandler.GetInvocationList().Contains(value))
                {
                    _listenEventHandler += value;
                }
            }
            remove
            {
                _listenEventHandler -= value;
            }
        }

        /// <summary>
        /// This Will Triggered Every Time Publisher dispatch event...
        /// And Will Trigger listenEventHandler
        /// </summary>
        /// <param name="sender">
        /// Sender Object
        /// </param>
        /// <param name="args">
        /// Arguments passed
        /// </param>
        public virtual void Listen(object sender, T args)
        {
            _listenEventHandler?.Invoke(this, args);
        }
    }
}
