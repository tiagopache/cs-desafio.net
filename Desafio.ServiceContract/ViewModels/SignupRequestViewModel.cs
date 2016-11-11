using Newtonsoft.Json;
using System.Collections.Generic;

namespace Desafio.Application.Contract.ViewModels
{
    public class SignupRequestViewModel
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }

        [JsonProperty("telefones")]
        public IList<TelefoneViewModel> Telefones { get; set; }
    }
}
