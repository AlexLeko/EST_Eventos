using AutoMapper;
using EventosIO.Application.Interfaces;
using EventosIO.Application.ViewModels;
using EventosIO.Domain.Core.Bus;
using EventosIO.Domain.Eventos.Commands;
using EventosIO.Domain.Eventos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventosIO.Application.Services
{
    public class EventoAppService : IEventoAppService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        private readonly IEventoRepository _eventoRepository;

        public EventoAppService(IBus bus, IMapper mapper, IEventoRepository eventoRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _eventoRepository = eventoRepository;
        }



        public void Registrar(EventoViewModel eventoViewModel)
        {
            var registroCommand = _mapper.Map<RegistrarEventoCommand>(eventoViewModel);
            _bus.SendCommand(registroCommand);
        }

        public void Atualizar(EventoViewModel eventoViewModel)
        {
            // TODO: validar organizador é dono do evento?

            var atualizarEventoCommand = _mapper.Map<AtualizarEventoCommand>(eventoViewModel);
            _bus.SendCommand(atualizarEventoCommand);
        }

        public void Excluir(Guid id)
        {
            _bus.SendCommand(new ExcluirEventoCommand(id));
        }

        #region [SELECT]

        public IEnumerable<EventoViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<EventoViewModel>>(_eventoRepository.ObterTodos());
        }

        public EventoViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<EventoViewModel>(_eventoRepository.ObterPorId(id));
        }

        public IEnumerable<EventoViewModel> ObterEventoPorOrganizador(Guid organizadorId)
        {
            return _mapper.Map<IEnumerable<EventoViewModel>>(_eventoRepository.ObterEventoPorOrganizador(organizadorId));
        }

        #endregion [SELECT]

        public void Dispose()
        {
            _eventoRepository.Dispose();
        }
    }
}
