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
        [DisplayName("ID")]
        public int ID { get; set; }



        [DisplayName("URL da Capa")]
        public string? CapaURL { get; set; }

        [DisplayName("URL do Fundo")]
        public string? FundoURL { get; set; }

        [DisplayName("URL da Loja")]
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

        [DisplayName("ID do Console")]
        public int? ConsoleID { get; set; }

        [DisplayName("Unidades")]
        public int? Unidade { get; set; }

        [DisplayName("Preço")]
        public float? Preco { get; set; }



        // Chaves estrangeiras:
        public Consoles? Consoles { get; set; }

        // É referenciado em:
        public ICollection<Alugueis>? Alugueis { get; set; }
    }
}
