using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LocadoraDejogos.Models
{
    public class Consoles
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [DisplayName("Fabricante")]
        public string? Fabricante { get; set; }

        [DisplayName("Geração")]
        public int? Geracao { get; set; }

        [DisplayName("Ano de Lançamento")]
        public int? Ano { get; set; }
    }
}
