﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Model
{
    [Table("Usuario")]
    public class Usuario : BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public DateTime UltimoLogin { get; set; }

        public string Token { get; set; }

        public virtual ICollection<Telefone> Telefones { get; set; }
    }
}
