using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.ServiceContract.ViewModels
{
    public class ErrorViewModel
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }
    }
}
