﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EventosIO.Domain.Core.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }


        public List<DomainNotification> GetNotification()
        {
            return _notifications;
        }

        public void Handle(DomainNotification message)
        {
            _notifications.Add(message);

            // teste
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro: { message.Key } - { message.Value }");
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }

    }
}
