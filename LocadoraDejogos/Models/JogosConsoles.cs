using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LocadoraDejogos.Models
{
    public class JogosConsoles
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("ID do Jogo")]
        public int? JogoID { get; set; }

        [DisplayName("ID do Console")]
        public int? ConsoleID { get; set; }



        // Chaves estrangeiras:
        public Jogos? Jogos { get; set; }
        public Consoles? Consoles { get; set; }
    }
}
