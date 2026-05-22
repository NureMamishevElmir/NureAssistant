using System.ComponentModel.DataAnnotations.Schema;
using DomainEntity.BaseEnitiy;
using DomainEntity.ChatEntities;

namespace DomainEntity.AIEntities;

public class AIFileChat : BaseEntity
{
    [ForeignKey(nameof(Chat))]
    public Guid ChatId { get; set; }
}
