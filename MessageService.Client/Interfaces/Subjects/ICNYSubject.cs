﻿using MessageService.Client.Interfaces.Publishers;
using MessageService.Client.Models;
using MessageService.Client.Publishers;
using MessageService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Client.Interfaces.Subjects
{
    public interface ICNYSubject : ISubject<ICNYPublisher, CurrencyModel>
    {
    }
}
