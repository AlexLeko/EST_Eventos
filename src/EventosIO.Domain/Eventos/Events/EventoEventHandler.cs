using EventosIO.Domain.Core.Events;
using EventosIO.Domain.Eventos.Events;
using System;

namespace EventosIO.Domain.Eventos.Events
{
    public class EventoEventHandler :
        IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>
    {
        public void Handle(EventoRegistradoEvent message)
        {
            // teste
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento registrado com sucesso!");

            // enviar e-mail
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            // teste
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento atualizado com sucesso!");

            // enviar e-mail
        }

        public void Handle(EventoExcluidoEvent message)
        {
            // teste
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Evento excluido com sucesso!");

            // enviar e-mail
        }
    }
}
