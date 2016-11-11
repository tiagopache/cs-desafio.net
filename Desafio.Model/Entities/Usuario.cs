using Desafio.Infrastructure.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Model.Entities
{
    [Table("Usuario")]
    public class Usuario : BaseIdEntity
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


        #region Singleton Pattern - Telefones
        private ICollection<Telefone> _telefones;
        public virtual ICollection<Telefone> Telefones
        {
            get
            {
                if (_telefones == null)
                    _telefones = new List<Telefone>();

                return _telefones;
            }
            set
            {
                _telefones = value;
            }
        }
        #endregion
    }
}
