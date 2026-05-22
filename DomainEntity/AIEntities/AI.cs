using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DomainEntity.BaseEnitiy;
using DomainEntity.CustomerEntities;

namespace DomainEntity.AIEntities;

public class AI : BaseEntity
{
    public required string Name { get; set; }

    public bool IsActive { get; set; }

    public DateTime ActivatedAt { get; set; }

    public DateTime DeactivatedAt { get; set; }

    [ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }

    [JsonIgnore]
    public virtual Customer Customer { get; set; }
}
