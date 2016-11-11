using Desafio.Application.Contract.ViewModels.Base;
using Desafio.Model.Entities;
using Newtonsoft.Json;

namespace Desafio.Application.Contract.ViewModels
{
    public class TelefoneViewModel : BaseViewModel<TelefoneViewModel, Telefone>
    {
        [JsonProperty("ddd")]
        public string Ddd { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }
    }
}
