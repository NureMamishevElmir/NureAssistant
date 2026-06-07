using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DomainEntity.AIEntities;
using DomainEntity.BaseEnitiy;
using DomainEntity.CustomerEntities;

namespace DomainEntity.FileEntities;

public class NureFile : BaseEntity
{
    public required string Name { get; set; }

    public required string Weight { get; set; }

    public bool IsActive { get; set; }

    public DateTime ActivatedAt { get; set; }

    public DateTime DeactivatedAt { get; set; }

    [ForeignKey(nameof(AI))]
    public Guid? AIId { get; set; }

    [JsonIgnore]
    public virtual AI? AI { get; set; }

    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }

    [JsonIgnore]
    public virtual Customer Customer { get; set; }
}
