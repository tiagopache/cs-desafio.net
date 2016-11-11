using Newtonsoft.Json;

namespace Desafio.Application.Contract.ViewModels
{
    public class ErrorViewModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }
    }
}
