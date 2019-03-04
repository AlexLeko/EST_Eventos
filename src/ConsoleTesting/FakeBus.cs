using EventosIO.Domain.Core.Bus;
using EventosIO.Domain.Core.Commands;
using EventosIO.Domain.Core.Events;
using EventosIO.Domain.Core.Notifications;
using EventosIO.Domain.Eventos.Commands;
using EventosIO.Domain.Eventos.Events;
using System;

public class FakeBus : IBus
{
    public void RaiseEvent<T>(T theEvent) where T : Event
    {      
        Publish(theEvent);
    }

    public void SendCommand<T>(T theCommand) where T : Command
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Commando { theCommand.MessageType } Lançado!");

        Publish(theCommand);
    }

    private static void Publish<T>(T message) where T : Message
    {
        var msgType = message.MessageType;

        // validar qual notification
        if (msgType.Equals("DomainNotification"))
        {
            var obj = new DomainNotificationHandler();
            ((IDomainNotificationHandler<T>)obj).Handle(message);
        }

        // validar qual Command
        if (msgType.Equals("RegistrarEventoCommand")
            || msgType.Equals("AtualizarEventoCommand")
            || msgType.Equals("ExcluirEventoCommand"))
        {
            var obj = new EventoCommandHandler(
                new FakeEventoRepository(),
                new FakeUow(),
                new FakeBus(),
                new DomainNotificationHandler()
            );

            ((IHandler<T>)obj).Handle(message);
        }

        // validar qual Event
        if (msgType.Equals("EventoRegistradoEvent") 
            || msgType.Equals("EventoAtualizadoEvent") 
            || msgType.Equals("EventoExcluidoEvent"))
        {
            var obj = new EventoEventHandler();
            ((IHandler<T>)obj).Handle(message);
        }
    }
}







