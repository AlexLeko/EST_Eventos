using EventosIO.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace EventosIO.Domain.Eventos
{
    public class Categoria : Entity<Categoria>
    {
        // prop
        public string Nome { get; private set; }

        // ef navigation
        public virtual ICollection<Evento> Eventos { get; set; }

        // contrutores
        public Categoria() { }

        public Categoria(Guid id)
        {
            Id = id;
        }

        // validadores
        public override bool EhValido()
        {
            return true;
        }
    }
}