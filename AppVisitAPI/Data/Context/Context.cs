using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Data.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arquivo>()
                .HasOne(arquivo => arquivo.Lugar)
                .WithOne(lugar => lugar.Arquivo)
                .HasForeignKey<Lugar>(lugar => lugar.ArquivoId);

            modelBuilder.Entity<Estado>()
                .HasOne(estado => estado.Pais)
                .WithMany(pais => pais.Estados)
                .HasForeignKey(estado => estado.PaisId);

            modelBuilder.Entity<Cidade>()
                .HasOne(cidade => cidade.Estado)
                .WithMany(estado => estado.Cidades)
                .HasForeignKey(cidade => cidade.EstadoId);

            modelBuilder.Entity<Lugar>()
                .HasOne(lugar => lugar.Cidade)
                .WithMany(cidade => cidade.Lugares)
                .HasForeignKey(lugar => lugar.CidadeId);
        }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Lugar> Lugares { get; set; }
        public DbSet<Pais> Paises { get; set; }
    }
}
