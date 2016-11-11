using Newtonsoft.Json;

namespace Desafio.Application.Contract.ViewModels
{
    public class LoginViewModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }
    }
}
