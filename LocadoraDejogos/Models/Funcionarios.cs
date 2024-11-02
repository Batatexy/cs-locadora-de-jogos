using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LocadoraDejogos.Models
{
    public class Funcionarios
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("CPF")]
        public string? CPF { get; set; }

        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [DisplayName("Data de Nascimento")]
        public string? DataNascimento { get; set; }



        // É referenciado em:
        public ICollection<Alugueis>? Alugueis { get; set; }
    }
}
