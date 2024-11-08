using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LocadoraDejogos.Models
{
    public class Clientes
    {
        [Key]
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Login")]
        public string? Login { get; set; }

        [DisplayName("Senha")]
        public string? Senha { get; set; }

        [DisplayName("CPF")]
        public string? CPF { get; set; }

        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [DisplayName("Data de Nascimento")]
        public string? DataNascimento { get; set; }

        [DisplayName("Telefone")]
        public string? Telefone { get; set; }



        // É referenciado em:
        public ICollection<Alugueis>? Alugueis { get; set; }
    }
}
