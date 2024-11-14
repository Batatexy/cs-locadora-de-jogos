using LocadoraDejogos.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDejogos.Data
{
    public class ApplicationDbContext : DbContext //herdamos a classe dbcontext, que controla o acesso ao BD
    {
        //criamos o construtor da nossa classe de contexto de BD com a possibilidade
        //de informar qual é o BD que vai ser usado através do parâmetro "option"
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON");
        }

        //adicionar um atributo do tipo dbset, que simboliza a tabela de BD aqui no C#
        //é necessário add um atributo dbset para cada tabela do BD

        public DbSet<Jogos> Jogos { get; set; }
        public DbSet<Consoles> Consoles { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Alugueis> Alugueis { get; set; }
        public DbSet<Privacy> Privacy { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogos>()
                .HasOne(j => j.Consoles) // Propriedade de navegação para Console
                .WithMany(c => c.Jogos) // Coleção de jogos na entidade Console
                .HasForeignKey(j => j.ConsoleID) // Chave estrangeira na tabela Jogos
                .OnDelete(DeleteBehavior.Cascade); // Comportamento de exclusão em cascata



            modelBuilder.Entity<Alugueis>()
                .HasOne(j => j.Clientes)
                .WithMany(c => c.Alugueis)
                .HasForeignKey(j => j.ClienteID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Alugueis>()
                .HasOne(j => j.Jogos)
                .WithMany(c => c.Alugueis)
                .HasForeignKey(j => j.JogoID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Alugueis>()
                .HasOne(j => j.Funcionarios)
                .WithMany(c => c.Alugueis)
                .HasForeignKey(j => j.FuncionarioID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}