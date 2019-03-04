using EventosIO.Domain.Eventos;
using EventosIO.Domain.Eventos.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public class FakeEventoRepository : IEventoRepository
{
    public void Adicionar(Evento obj)
    {
        //
    }
       
    public void AdicionarEndereco(Endereco endereco)
    {
        throw new NotImplementedException();
    }
    
    public void AtualizarEndereco(Endereco endereco)
    {
        throw new NotImplementedException();
    }
    

    public IEnumerable<Evento> Buscar(Expression<Func<Evento, bool>> predicate)
    {
        throw new NotImplementedException();
    }
    
    public Evento ObterPorId(Guid id)
    {
        return new Evento("Fake", DateTime.Now, DateTime.Now, true, 0, true, "Empresa");
    }

    public Endereco ObterEndereco(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId)
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<Evento> ObterTodos()
    {
        throw new NotImplementedException();
    }

    public void Remover(Guid id)
    {
        //
    }
    
    public int SaveChanges()
    {
        throw new NotImplementedException();
    }

    public void Atualizar(Evento obj)
    {
        //
    }


    public void Dispose()
    {
        //
    }
}







