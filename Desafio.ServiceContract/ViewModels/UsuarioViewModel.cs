using Desafio.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.ServiceContract.ViewModels
{
    public class UsuarioViewModel : BaseViewModel<UsuarioViewModel, Usuario>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }

        [JsonProperty("data_criacao")]
        public DateTime DataCriacao { get; set; }

        [JsonProperty("data_atualizacao")]
        public DateTime DataAtualizacao { get; set; }

        [JsonProperty("ultimo_login")]
        public DateTime UltimoLogin { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("telefones")]
        public IList<TelefoneViewModel> Telefones { get; set; }
    }
}
