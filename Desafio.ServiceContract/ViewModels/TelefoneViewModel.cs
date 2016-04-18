using Desafio.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.ServiceContract.ViewModels
{
    public class TelefoneViewModel : BaseViewModel<TelefoneViewModel, Telefone>
    {
        [JsonProperty("ddd")]
        public string Ddd { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }
    }
}
