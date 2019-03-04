using EventosIO.Domain.Core.Commands;
using EventosIO.Domain.Interfaces;
using System;

public class FakeUow : IUnitOfWork
{
    public CommandResponse Commit()
    {
        return new CommandResponse(true);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}







