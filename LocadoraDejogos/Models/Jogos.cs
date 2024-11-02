using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDejogos.Models
{
    public class Jogos
    {
        [Key]
        public int ID { get; set; }

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

        [DisplayName("ID do Console")]
        public int? ConsoleID { get; set; }
    }
}
