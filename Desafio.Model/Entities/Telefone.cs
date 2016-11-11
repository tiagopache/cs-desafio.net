using Desafio.Infrastructure.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Model.Entities
{
    [Table("Telefone")]
    public class Telefone : BaseIdEntity
    {
        [Required]
        [RegularExpression("[0-9]{3}")]
        public string Ddd { get; set; }

        [Required]
        [Phone]
        public string Numero { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
