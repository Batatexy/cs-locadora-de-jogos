using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LocadoraDejogos.Models
{
    public class Privacy
    {
        [Key]
        [DisplayName("ID")]
        public int ID { get; set; }
    }
}
