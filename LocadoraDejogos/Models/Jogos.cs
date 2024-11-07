using IGDB;
using IGDB.Models;
using LocadoraDejogos.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDejogos.Models
{
    public class Jogos
    {
        [Key]
        public int ID { get; set; }



        [DisplayName("Capa")]
        public string? CapaURL { get; set; }

        public string? FundoURL { get; set; }

        public string? LojaURL { get; set; }



        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [DisplayName("Desenvolvedor")]
        public string? Desenvolvedor { get; set; }

        [DisplayName("Distribuidora")]
        public string? Distribuidora { get; set; }

        [DisplayName("Gênero")]
        public string? Genero { get; set; }

        [DisplayName("Ano de Lançamento")]
        public int? Ano { get; set; }

        [DisplayName("Unidade")]
        public int? Unidade { get; set; }

        //Atrelado à 1 console
        [DisplayName("ID do Console")]
        public int? ConsoleID { get; set; }

        //Atrelado à vários, ainda em testes
        [DisplayName("JogosConsolesID")]
        public int? JogosConsolesID { get; set; }


        // Chaves estrangeiras:
        public Consoles? Consoles { get; set; }

        // É referenciado em:
        public ICollection<Alugueis>? Alugueis { get; set; }
        public ICollection<JogosConsoles>? JogosConsoles { get; set; }
    }
}
