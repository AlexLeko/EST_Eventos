using EventosIO.Domain.Core.Models;
using EventosIO.Domain.Eventos;
using System;
using System.Collections.Generic;

namespace EventosIO.Domain.Organizadores
{
    public class Organizador : Entity<Organizador>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }

        // PROP EF NAVIGATION
        public virtual ICollection<Evento> Eventos { get; set; }


        // EF CONSTRUTOR
        public Organizador() { }

        public Organizador(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }



        public override bool EhValido()
        {
            return true;
        }
    }
}