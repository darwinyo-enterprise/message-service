using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Interfaces.Subjects;
using MessageService.Client.Interfaces.Subscribers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Interfaces
{
    public interface ICurrencyUnitOfWork
    {
        IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// Currency Publisher
        /// </summary>
        ICNYPublisher CNYPublisher { get; set; }
        IGBPPublisher GBPPublisher { get; set; }
        IJPYPublisher JPYPublisher { get; set; }

        /// <summary>
        /// Currency Subject
        /// </summary>
        IJPYSubject JPYSubject { get; set; }
        IGBPSubject GBPSubject { get; set; }
        ICNYSubject CNYSubject { get; set; }


        #region Subscribers
        IUSSubscriber USSubscriber { get; set; }
        IJPNSubscriber JPNSubscriber { get; set; }
        ICNSubscriber CNSubscriber { get; set; }
        #endregion
    }
}
