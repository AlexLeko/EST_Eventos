using EventoIO.Infra.Data.Context;
using EventosIO.Domain.Core.Commands;
using EventosIO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventoIO.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventosContext _context;

        public UnitOfWork(EventosContext context)
        {
            _context = context;
        }


        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
