using EventosIO.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventosIO.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}
