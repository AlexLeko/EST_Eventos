using EventosIO.Domain.Core.Models;
using FluentValidation;
using System;

namespace EventosIO.Domain.Eventos
{
    public class Endereco : Entity<Endereco>
    {
        #region [ PROPRIEDADES ]

        public string Logradouro { get; private set; }

        public string Numero { get; private set; }

        public string Complemento { get; private set; }

        public string Bairro { get; private set; }

        public string CEP { get; private set; }

        public string Cidade { get; private set; }

        public string Estado { get; private set; }

        public Guid? EventoID { get; private set; }

        #endregion [ PROPRIEDADES ]
        

        #region [ Entity Framework ]
        public virtual Evento Evento { get; private set; }

        #endregion [ Entity Framework ]


        #region [ CONTRUTORES ]

        protected Endereco() { }

        public Endereco(
            Guid id,
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string cep,
            string cidade,
            string estado,
            Guid? eventoID
        )
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            EventoID = eventoID;
        }

        #endregion [ CONTRUTORES ]


        #region [ VALIDADORES ]
        public override bool EhValido()
        {
            RuleFor(e => e.Logradouro)
                .NotEmpty().WithMessage("O logradouro deve se fornecido.")
                .Length(2, 150).WithMessage("O logradouro deve ter 2 e 150 letras.");

            RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("O bairro deve se fornecido.")
                .Length(2, 150).WithMessage("O bairro deve ter 2 e 150 letras.");

            RuleFor(e => e.CEP)
                .NotEmpty().WithMessage("O CEP deve se fornecido.")
                .Length(8).WithMessage("O CEP deve ter 8 numeros.");

            RuleFor(e => e.Cidade)
                .NotEmpty().WithMessage("A Cidade deve se fornecido.")
                .Length(2, 150).WithMessage("A Cidade deve ter 2 e 150 letras.");

            RuleFor(e => e.Estado)
                .NotEmpty().WithMessage("O Estado deve se fornecido.")
                .Length(2, 150).WithMessage("O Estado deve ter 2 e 150 letras.");

            RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("O Numero deve se fornecido.")
                .Length(1, 10).WithMessage("O Numero deve ter entre 1 e 10 numeros.");

            RuleFor(e => e.Complemento)
                .NotEmpty().WithMessage("O Complemento deve se fornecido.")
                .Length(2, 150).WithMessage("O Complemento deve ter 2 e 150 letras.");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        #endregion [ VALIDADORES ]


    }
}