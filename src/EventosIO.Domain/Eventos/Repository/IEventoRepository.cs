using EventosIO.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace EventosIO.Domain.Eventos.Repository
{
    public interface IEventoRepository : IRepository<Evento>
    {

        #region [ AGREGAÇÃO ]

        // Organizador
        IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId);


        // Endereco
        Endereco ObterEndereco(Guid id);

        void AdicionarEndereco(Endereco endereco);

        void AtualizarEndereco(Endereco endereco);

        #endregion [ AGREGAÇÃO ]
    }
}
