using EventosIO.Domain.Core.Bus;
using EventosIO.Domain.Core.Notifications;
using EventosIO.Domain.Interfaces;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosIO.Domain.CommandHandlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        protected CommandHandler(IUnitOfWork uow, IBus bus,
            IDomainNotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _bus = bus;
            _notifications = notifications;
        }


        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                Console.WriteLine(erro.ErrorMessage);
                _bus.RaiseEvent(new DomainNotification(erro.PropertyName, erro.ErrorMessage));
            }
        }


        protected bool Commit()
        {
            if (_notifications.HasNotification()) return false;

            var commandResponse = _uow.Commit();
            if (commandResponse.Sucess) return true;

            Console.WriteLine("Ocorreu um erro ao salvar os dados no banco");
            _bus.RaiseEvent(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados no banco"));

            return false;
        }

    }
}
