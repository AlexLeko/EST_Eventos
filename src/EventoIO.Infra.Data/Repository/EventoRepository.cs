using EventoIO.Infra.Data.Context;
using EventosIO.Domain.Eventos;
using EventosIO.Domain.Eventos.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventoIO.Infra.Data.Repository
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(EventosContext context) : base(context)
        {

        }

        public void AdicionarEndereco(Endereco endereco)
        {
            DB.Enderecos.Add(endereco);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            DB.Enderecos.Update(endereco);
        }

        public Endereco ObterEndereco(Guid id)
        {
            return DB.Enderecos.Find(id);
        }

        public IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId)
        {
            return DB.Eventos.Where(e => e.OrganizadorID == organizadorId);
        }

        // Retorna um Evento com o Endereço pelo Id. (JOIN)
        public override Evento ObterPorId(Guid id)
        {
            return DB.Eventos.Include(e => e.Endereco).FirstOrDefault(e => e.Id == id);
        }

    }
}
