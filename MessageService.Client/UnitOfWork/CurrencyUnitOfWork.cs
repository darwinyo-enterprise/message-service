using MessageService.Client.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Interfaces.Subjects;
using MessageService.Client.Interfaces.Subscribers;

namespace MessageService.Client.UnitOfWork
{
    public class CurrencyUnitOfWork : ICurrencyUnitOfWork
    {
        #region Private Fields
        /// <summary>
        /// DI Container Provider
        /// </summary>
        private IServiceProvider _serviceProvider;

        private IJPYPublisher _jPYPublisher;
        private IGBPPublisher _gBPPublisher;
        private ICNYPublisher _cNYPublisher;

        private IJPYSubject _jPYSubject;
        private IGBPSubject _gBPSubject;
        private ICNYSubject _cNYSubject;

        private ICNSubscriber _cNSubscriber;
        private IUSSubscriber _uSSubscriber;
        private IJPNSubscriber _jPNSubscriber;

        #endregion

        #region Properties
        public IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
            set
            {
                _serviceProvider = value;
            }
        }

        #region Publishers
        public IJPYPublisher JPYPublisher
        {
            get
            {
                if (_jPYPublisher == null)
                {
                    _jPYPublisher = _serviceProvider.GetService<IJPYPublisher>();
                }
                return _jPYPublisher;
            }
            set
            {
                _jPYPublisher = value;
            }
        }
        public IGBPPublisher GBPPublisher
        {
            get
            {
                if (_gBPPublisher == null)
                {
                    _gBPPublisher = _serviceProvider.GetService<IGBPPublisher>();
                }
                return _gBPPublisher;
            }
            set
            {
                _gBPPublisher = value;
            }
        }
        public ICNYPublisher CNYPublisher
        {
            get
            {
                if (_cNYPublisher == null)
                {
                    _cNYPublisher = _serviceProvider.GetService<ICNYPublisher>();
                }
                return _cNYPublisher;
            }
            set
            {
                _cNYPublisher = value;
            }
        }
        #endregion

        #region Subjects
        public ICNYSubject CNYSubject
        {
            get
            {
                if (_cNYSubject == null)
                {
                    _cNYSubject = _serviceProvider.GetService<ICNYSubject>();
                }
                return _cNYSubject;
            }
            set
            {
                _cNYSubject = value;
            }
        }
        public IJPYSubject JPYSubject
        {
            get
            {
                if (_jPYSubject == null)
                {
                    _jPYSubject = _serviceProvider.GetService<IJPYSubject>();
                }
                return _jPYSubject;
            }
            set
            {
                _jPYSubject = value;
            }
        }
        public IGBPSubject GBPSubject
        {
            get
            {
                if (_gBPSubject == null)
                {
                    _gBPSubject = _serviceProvider.GetService<IGBPSubject>();
                }
                return _gBPSubject;
            }
            set
            {
                _gBPSubject = value;
            }
        }
        #endregion

        #region Subscribers
        public IUSSubscriber USSubscriber
        {
            get
            {
                if (_uSSubscriber == null)
                {
                    _uSSubscriber = _serviceProvider.GetService<IUSSubscriber>();
                }
                return _uSSubscriber;
            }
            set
            {
                _uSSubscriber = value;
            }
        }
        public IJPNSubscriber JPNSubscriber
        {
            get
            {
                if (_jPNSubscriber == null)
                {
                    _jPNSubscriber = _serviceProvider.GetService<IJPNSubscriber>();
                }
                return _jPNSubscriber;
            }
            set
            {
                _jPNSubscriber = value;
            }
        }
        public ICNSubscriber CNSubscriber
        {
            get
            {
                if (_cNSubscriber == null)
                {
                    _cNSubscriber = _serviceProvider.GetService<ICNSubscriber>();
                }
                return _cNSubscriber;
            }
            set
            {
                _cNSubscriber = value;
            }
        }
        #endregion

        #endregion
    }
}
