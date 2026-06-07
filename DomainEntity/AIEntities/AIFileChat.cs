using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DomainEntity.BaseEnitiy;
using DomainEntity.ChatEntities;
using DomainEntity.FileEntities;

namespace DomainEntity.AIEntities;

public class AIFileChat : BaseEntity
{
    [ForeignKey(nameof(Chat))]
    public Guid ChatId { get; set; }

    [JsonIgnore]
    public virtual Chat Chat { get; set; }

    [ForeignKey(nameof(File))]
    public Guid FileId { get; set; }

    [JsonIgnore]
    public virtual NureFile File { get; set; }
}
