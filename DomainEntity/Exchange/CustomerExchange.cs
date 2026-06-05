using DomainEntity.CustomerEntities.Enums;

namespace DomainEntity.Exchange;

public class CustomerExchange
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public CustomerType Type { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
