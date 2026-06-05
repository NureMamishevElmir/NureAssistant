using DomainEntity.CustomerEntities.Enums;

namespace DomainEntity.Request;

public class CustomerRequest
{
    public string? Name { get; set; }

    public CustomerType Type { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }
}
