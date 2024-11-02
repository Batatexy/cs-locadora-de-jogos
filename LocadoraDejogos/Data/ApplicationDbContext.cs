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

        public class Funcionarios
        {
            public int ID { get; set; } // Chave primária
            public string? CPF { get; set; }
            public string? Nome { get; set; }
            public string? DataNascimento { get; set; }

            // É referenciado em:
            public ICollection<Alugueis>? Alugueis { get; set; }
        }

        public class Clientes
        {
            public int ID { get; set; }
            public string? CPF { get; set; }
            public string? Nome { get; set; }
            public string? DataNascimento { get; set; }
            public string? Telefone { get; set; }

            // É referenciado em:
            public ICollection<Alugueis>? Alugueis { get; set; }
        }

        public class JogosConsoles
        {
            public int ID { get; set; }
            public Jogos? JogoID { get; set; }
            public Consoles? ConsoleID { get; set; }
            
            // Chaves estrangeiras:
            public Jogos? Jogos { get; set; }
            public Consoles? Consoles { get; set; }
        }

        public class Alugueis
        {
            public int ID { get; set; }
            public Clientes? ClienteID { get; set; }
            public Jogos? JogoID { get; set; } 
            public Funcionarios? FuncionarioID { get; set; }

            public Clientes? Clientes { get; set; }
            public Jogos? Jogos { get; set; }
            public Funcionarios? Funcionarios { get; set; }
        }

        public class Jogos
        {
            public int ID { get; set; }
            public string? Nome { get; set; }
            public string? Desenvolvedor { get; set; }
            public string? Distribuidora { get; set; }
            public string? Genero { get; set; }
            public int? Ano { get; set; }
            public int? Unidade { get; set; }
            public int ConsoleID { get; set; }

            // Chaves estrangeiras:
            public Consoles? Consoles { get; set; }

            // É referenciado em:
            public ICollection<Alugueis>? Alugueis { get; set; }
            public ICollection<JogosConsoles>? JogosConsoles { get; set; }
        }

        public class Consoles
        {
            public int? ID { get; set; }
            public string? Nome { get; set; }
            public string? Fabricante { get; set; }
            public int? Geracao { get; set; }
            public int? Ano { get; set; }

            public ICollection<Jogos>? Jogos { get; set; }
            public ICollection<JogosConsoles>? JogosConsoles { get; set; }
        }

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



            modelBuilder.Entity<JogosConsoles>()
                .HasOne(j => j.Jogos)
                .WithMany(c => c.JogosConsoles)
                .HasForeignKey(j => j.JogoID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JogosConsoles>()
                .HasOne(j => j.Consoles)
                .WithMany(c => c.JogosConsoles)
                .HasForeignKey(j => j.ConsoleID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}