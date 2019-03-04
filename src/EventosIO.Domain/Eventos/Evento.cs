using EventosIO.Domain.Core.Models;
using EventosIO.Domain.Organizadores;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace EventosIO.Domain.Eventos
{
    public class Evento : Entity<Evento>
    {
        public Evento(
            string nome,
            DateTime dataInicio,
            DateTime dataFim,
            bool gratuito,
            decimal valor,
            bool online,
            string nomeEmpresa
            )
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;
        }

        // Para uso da Factory
        private Evento() { }

        public string Nome { get; private set; }

        public string DescricaoCurta { get; private set; }

        public string DescricaoLonga { get; private set; }

        public DateTime DataInicio { get; private set; }

        public DateTime DataFim { get; private set; }

        public bool Gratuito { get; private set; }

        public decimal Valor { get; private set; }

        public bool Online { get; private set; }

        public string NomeEmpresa { get; private set; }

        public bool Excluido { get; private set; }

        public ICollection<Tags> Tags { get; private set; }

        public Guid? CategoriaID { get; private set; }

        public Guid? EnderecoID { get; private set; }

        public Guid OrganizadorID { get; private set; }



        #region [ Entity Framework ]

        public virtual Categoria Categoria { get; private set; }

        public virtual Endereco Endereco { get; private set; }

        public virtual Organizador Organizador { get; private set; }

        #endregion [ Entity Framework ]


        #region [ MANIPULAR AGREGAÇÕES ]

        public void AtribuirEndereco(Endereco endereco)
        {
            if (!endereco.EhValido()) return;
            Endereco = endereco;
        }

        public void AtribuirCategoria(Categoria categoria)
        {
            if (!categoria.EhValido()) return;
            Categoria = categoria;
        }

        public void ExcluirEvento()
        {
            // TODO: validar RN para exclusao do evento?

            Excluido = true;
            


        }

        #endregion [ MANIPULAR AGREGAÇÕES ]



        #region VALIDAÇÃO

        public override bool EhValido()
        {
            Validar();

            return ValidationResult.IsValid;
        }

        private void Validar()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarNomeEmpresa();

            ValidationResult = Validate(this);

            // validações adicionais
            ValidarEndereco();
        }

        private void ValidarNome()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O evento deve ter um nome.")
                .Length(2, 150).WithMessage("O evento deve ter 2 e 150 letras.");
        }

        private void ValidarValor()
        {
            if (!Gratuito)
                RuleFor(e => e.Valor)
                    .InclusiveBetween(1, 50000)
                    .WithMessage("Valor do evento deve estar entre 1 e 50.000 reais.");

            if (Gratuito)
                RuleFor(e => e.Valor)
                    .InclusiveBetween(0, 0).When(e => e.Gratuito)
                    .WithMessage("O evento é gratuito, não deve ter valor!");
        }

        private void ValidarData()
        {
            RuleFor(e => e.DataInicio)
                .LessThan(e => e.DataFim)
                .WithMessage("Data inicial esta maior que a data final do evento.");

            RuleFor(e => e.DataInicio)
               .GreaterThan(DateTime.Now)
               .WithMessage("Data inicial inferior a data atual.");
        }

        private void ValidarLocal()
        {
            if (Online)
                RuleFor(e => e.Endereco)
                    .Null().When(e => e.Online)
                    .WithMessage("Evento online não contem endereço.");

            if (!Online)
                RuleFor(e => e.Endereco)
                   .NotNull().When(e => e.Online == false)
                   .WithMessage("O evento deve possuir um endereço.");
        }

        private void ValidarNomeEmpresa()
        {
            RuleFor(e => e.NomeEmpresa)
                .NotEmpty().WithMessage("O organizador deve ter um nome.")
                .Length(2, 150).WithMessage("O nome do organizador deve ter 2 e 150 letras.");
        }

        // =======================
        //      AGREGAÇÕES
        // =======================

        private void ValidarEndereco()
        {
            if (Online) return;
            if (Endereco.EhValido()) return;

            foreach (var erro in Endereco.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(erro);
            }
        }

        #endregion VALIDAÇÃO


        #region [ FACTORY ]

        public static class EventoFactory
        {
            public static Evento NovoEventoCompleto(
                Guid id, string nome, string descCurta, string descLonga,
                DateTime dataInicio, DateTime dataFim, bool gratuito,
                decimal valor, bool online, string nomeEmpresa, Guid? organizadorId, 
                Endereco endereco, Guid categoriaID)
            {
                // construtor privado
                var evento = new Evento()
                {
                    Id = id,
                    Nome = nome,
                    DescricaoCurta = descCurta,
                    DescricaoLonga = descLonga,
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    Gratuito = gratuito,
                    Valor = valor,
                    Online = online,
                    NomeEmpresa = nomeEmpresa,
                    Endereco = endereco,
                    CategoriaID = categoriaID
                };

                if (organizadorId != null)
                    evento.Organizador = new Organizador(organizadorId.Value);

                if (online)
                    evento.Endereco = null;

                return evento;
            }
        }

        #endregion [ FACTORY ]

    }




}
