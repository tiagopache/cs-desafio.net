using Desafio.Application.Contract.ViewModels.Base;
using Desafio.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Desafio.Application.Contract.ViewModels
{
    public class UsuarioViewModel : BaseViewModel<UsuarioViewModel, Usuario>
    {
        [JsonProperty("id", Order = 0)]
        public int Id { get; set; }

        [JsonProperty("nome", Order = 1)]
        public string Nome { get; set; }

        [JsonProperty("email", Order = 2)]
        public string Email { get; set; }

        [JsonProperty("senha", Order = 3)]
        public string Senha { get; set; }

        [JsonProperty("data_criacao", Order = 4)]
        public DateTime DataCriacao { get; set; }

        [JsonProperty("data_atualizacao", Order = 5)]
        public DateTime DataAtualizacao { get; set; }

        [JsonProperty("ultimo_login", Order = 6)]
        public DateTime UltimoLogin { get; set; }

        [JsonProperty("token", Order = 7)]
        public string Token { get; set; }

        #region Singleton Pattern - Telefones
        private IList<TelefoneViewModel> _telefones;
        [JsonProperty("telefones", Order = 8)]
        public IList<TelefoneViewModel> Telefones
        {
            get
            {
                if (_telefones == null)
                    _telefones = new List<TelefoneViewModel>();

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
