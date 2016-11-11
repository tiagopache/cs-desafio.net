using System.ComponentModel.DataAnnotations;

namespace Desafio.Infrastructure.Data.Base
{
    public abstract class BaseConcurrencyIdEntity : BaseIdEntity
    {
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
