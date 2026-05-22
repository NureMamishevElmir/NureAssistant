using System.Collections.ObjectModel;
using DomainEntity.AIEntities;
using DomainEntity.BaseEnitiy;
using DomainEntity.ChatEntities;
using DomainEntity.CustomerEntities.Enums;
using DomainEntity.FileEntities;

namespace DomainEntity.CustomerEntities;

public class Customer : BaseEntity
{
    public string? Name { get; set; }

    public CustomerType Type { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public ICollection<AI>? AIs { get; set; } = new Collection<AI>();

    public ICollection<NureFile>? Files { get; set; } = new Collection<NureFile>();

    public ICollection<Chat>? Chats { get; set; } = new Collection<Chat>();

    public ICollection<Message> Messages { get; set; } = new Collection<Message>();
}
