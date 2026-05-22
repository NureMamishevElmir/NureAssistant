using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DomainEntity.BaseEnitiy;
using DomainEntity.FileEntities;

namespace DomainEntity.AIEntities;

public class AIFile : BaseEntity
{
    [ForeignKey(nameof(AI))]
    public Guid AIId { get; set; }

    [JsonIgnore]
    public virtual AI AI { get; set; }

    [ForeignKey(nameof(File))]
    public Guid FileId { get; set; }

    [JsonIgnore]
    public virtual NureFile File { get; set; }
}
