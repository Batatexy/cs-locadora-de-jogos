using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LocadoraDejogos.Models
{
    public class Consoles
    {
        [Key]

        [DisplayName("ID")]
        public int ID { get; set; }


        [DisplayName("URL da Imagem")]
        public string? ImagemURL { get; set; }

        [DisplayName("URL da Wikipedia")]
        public string? WikipediaURL { get; set; }


        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [DisplayName("Fabricante")]
        public string? Fabricante { get; set; }

        [DisplayName("Geração")]
        public int? Geracao { get; set; }

        [DisplayName("Ano de Lançamento")]
        public int? Ano { get; set; }




        public ICollection<Jogos>? Jogos { get; set; }
    }
}
