using EventosIO.Domain.Core.Commands;
using EventosIO.Domain.Core.Events;
using System;

namespace EventosIO.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command; 

        void RaiseEvent<T>(T theEvent) where T : Event; 
    }
}
