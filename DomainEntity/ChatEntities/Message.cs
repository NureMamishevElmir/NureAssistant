using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DomainEntity.BaseEnitiy;
using DomainEntity.CustomerEntities;
using DomainEntity.FileEntities;

namespace DomainEntity.ChatEntities;

public class Message : BaseEntity
{
    public required string Text { get; set; }

    [ForeignKey(nameof(Customer))]
    public Guid? CustomerId { get; set; }

    [JsonIgnore]
    public virtual Customer? Customer { get; set; }

    [ForeignKey(nameof(Chat))]
    public Guid ChatId { get; set; }

    [JsonIgnore]
    public virtual Chat Chat { get; set; }

    [ForeignKey(nameof(File))]
    public Guid? FileId { get; set; }

    [JsonIgnore]
    public virtual NureFile? File { get; set; }
}
