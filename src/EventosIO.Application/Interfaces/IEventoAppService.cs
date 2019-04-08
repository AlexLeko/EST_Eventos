using EventosIO.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace EventosIO.Application.Interfaces
{
    public interface IEventoAppService : IDisposable
    {

        void Registrar(EventoViewModel eventoViewModel);

        IEnumerable<EventoViewModel> ObterTodos();

        IEnumerable<EventoViewModel> ObterEventoPorOrganizador(Guid organizadorId);

        EventoViewModel ObterPorId(Guid Id);

        void Atualizar(EventoViewModel eventoViewModel);

        void Excluir(Guid id);

    }
}
