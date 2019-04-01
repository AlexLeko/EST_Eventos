using EventoIO.Infra.Data.Extensions;
using EventoIO.Infra.Data.Mappings;
using EventosIO.Domain.Eventos;
using EventosIO.Domain.Organizadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EventoIO.Infra.Data.Context
{
    public class EventosContext : DbContext
    {
        #region [DBSET]
        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Organizador> Organizadores { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        #endregion [DBSET]



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region [FLUENT API]  

            modelBuilder.AddConfiguration(new EventoMapping());
            modelBuilder.AddConfiguration(new OrganizadorMapping());
            modelBuilder.AddConfiguration(new EnderecoMapping());
            modelBuilder.AddConfiguration(new CategoriaMapping());

            #endregion [FLUENT API]


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

    }
}
