﻿using EventosIO.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosIO.Domain.Core.Notifications
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        bool HasNotification();

        List<T> GetNotification();

    }
}
