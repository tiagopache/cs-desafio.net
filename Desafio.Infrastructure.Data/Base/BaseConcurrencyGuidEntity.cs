using System.ComponentModel.DataAnnotations;

namespace Desafio.Infrastructure.Data.Base
{
    public abstract class BaseConcurrencyGuidEntity : BaseGuidEntity
    {
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
