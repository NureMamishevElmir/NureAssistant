using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DomainEntity.BaseEnitiy;
using DomainEntity.ChatEntities;

namespace DomainEntity.AIEntities;

public class AIFileChat : BaseEntity
{
    [ForeignKey(nameof(Chat))]
    public Guid ChatId { get; set; }

    [JsonIgnore]
    public virtual Chat Chat { get; set; }

    [ForeignKey(nameof(AIFile))]
    public Guid AIFileId { get; set; }

    [JsonIgnore]
    public virtual AIFile AIFile { get; set; }
}
