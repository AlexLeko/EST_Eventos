using EventoIO.Infra.Data.Extensions;
using EventosIO.Domain.Eventos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventoIO.Infra.Data.Mappings
{
    public class EnderecoMapping : EntityTypeConfiguration<Endereco>
    {
        public override void Map(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");


            // PROPRIEDADES
            builder.Property(e => e.Logradouro)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.Numero)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(e => e.Bairro)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.CEP)
                .HasColumnType("varchar(8)")
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(e => e.Complemento)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Cidade)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Estado)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();


            // RELACIONAMENTOS
            builder.HasOne(e => e.Evento)
                .WithOne(o => o.Endereco)
                .HasForeignKey<Endereco>(e => e.EventoID)
                .IsRequired(false);

            // IGNORE
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

        }
    }
}
