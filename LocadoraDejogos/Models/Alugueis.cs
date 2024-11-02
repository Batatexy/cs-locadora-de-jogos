using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LocadoraDejogos.Models
{
    public class Alugueis
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("ID do Cliete")]
        public int? ClienteID { get; set; }

        [DisplayName("ID do Jogo")]
        public int? JogoID { get; set; }

        [DisplayName("ID do Funcionário")]
        public int? FuncionarioID { get; set; }


        // Chaves estrangeiras:
        public Clientes? Clientes { get; set; }
        public Jogos? Jogos { get; set; }
        public Funcionarios? Funcionarios { get; set; }

    }
}
