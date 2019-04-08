using EventosIO.Domain.CommandHandlers;
using EventosIO.Domain.Core.Bus;
using EventosIO.Domain.Core.Events;
using EventosIO.Domain.Core.Notifications;
using EventosIO.Domain.Eventos.Events;
using EventosIO.Domain.Eventos.Repository;
using EventosIO.Domain.Interfaces;
using System;

namespace EventosIO.Domain.Eventos.Commands
{
    public class EventoCommandHandler : CommandHandler,
        IHandler<RegistrarEventoCommand>,
        IHandler<AtualizarEventoCommand>,
        IHandler<ExcluirEventoCommand>
    {

        #region [ IoC ]
        private readonly IEventoRepository _eventoRepository;
        private readonly IBus _bus;

        public EventoCommandHandler(IEventoRepository eventoRepository,
                                    IUnitOfWork uow,
                                    IBus bus,
                                    IDomainNotificationHandler<DomainNotification> notifications
        ) : base(uow, bus, notifications)
        {
            _eventoRepository = eventoRepository;
            _bus = bus;
        }
        #endregion [ IoC ]

        #region [ HANDLE ]

        #region [ Registrar Evento ]
        public void Handle(RegistrarEventoCommand message)
        {
            var endereco = new Endereco(message.Endereco.Id, message.Endereco.Logradouro, message.Endereco.Numero,
                message.Endereco.Complemento, message.Endereco.Bairro, message.Endereco.CEP, message.Endereco.Cidade, 
                message.Endereco.Estado, message.Endereco.EventoID.Value);

            var evento = Evento.EventoFactory.NovoEventoCompleto(
                message.Id,
                message.Nome,
                message.DescricaoCurta,
                message.DescricaoLonga,
                message.DataInicio,
                message.DataFim,
                message.Gratuito,
                message.Valor,
                message.Online,
                message.NomeEmpresa,
                message.OrganizadorID,
                endereco,
                message.CategoriaId
            );

            if (!EventoValido(evento)) return;


            // validações


            // persistir
            _eventoRepository.Adicionar(evento);

            if (Commit())
            {
                //Console.WriteLine("Evento registrado com sucesso");
                _bus.RaiseEvent(new EventoRegistradoEvent(
                    evento.Id,
                    evento.Nome,
                    evento.DataInicio,
                    evento.DataFim,
                    evento.Gratuito,
                    evento.Valor,
                    evento.Online,
                    evento.NomeEmpresa
                ));
            }
        }
        
        #endregion [ Registrar Evento ]

        #region [ Atualizar Evento ]
        public void Handle(AtualizarEventoCommand message)
        {
            var eventoAtual = _eventoRepository.ObterPorId(message.Id);

            if (!EventoExistente(message.Id, message.MessageType)) return;

            // todo: validar se evento pertence ao editor.

            var evento = Evento.EventoFactory.NovoEventoCompleto(
                message.Id,
                message.Nome,
                message.DescricaoCurta,
                message.DescricaoLonga,
                message.DataInicio,
                message.DataFim,
                message.Gratuito,
                message.Valor,
                message.Online,
                message.NomeEmpresa,
                message.OrganizadorID,
                eventoAtual.Endereco,
                message.CategoriaId
                
            );

            if (!EventoValido(evento)) return;

            _eventoRepository.Atualizar(evento);

            if (Commit())
            {
                _bus.RaiseEvent(new EventoAtualizadoEvent(
                    evento.Id,
                    evento.Nome,
                    evento.DescricaoCurta,
                    evento.DescricaoLonga,
                    evento.DataInicio,
                    evento.DataFim,
                    evento.Gratuito,
                    evento.Valor,
                    evento.Online,
                    evento.NomeEmpresa
                ));
            }
        }
        
        #endregion [ Atualizar Evento ]

        #region [ Excluir Evento ]
        public void Handle(ExcluirEventoCommand message)
        {
            if (!EventoExistente(message.Id, message.MessageType)) return;

            _eventoRepository.Remover(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new EventoExcluidoEvent(message.Id));
            }
        }
        
        #endregion [ Excluir Evento ]

        #endregion [ HANDLE ]


        #region [ VALIDADORES ]

        private bool EventoValido(Evento evento)
        {
            if (evento.EhValido()) return true;

            NotificarValidacoesErro(evento.ValidationResult);
            return false;
        }

        private bool EventoExistente(Guid id, string messageType)
        {
            var evento = _eventoRepository.ObterPorId(id);

            if (evento != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Evento não encontrado"));
            return false;
        }

        #endregion [ VALIDADORES ]

    }
}
