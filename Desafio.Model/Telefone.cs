using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    [Table("Telefone")]
    public class Telefone: BaseEntity
    {
        [Required]
        [RegularExpression("d{3}")]
        public string Ddd { get; set; }

        [Required]
        [Phone]
        public string Numero { get; set; }
    }
}
