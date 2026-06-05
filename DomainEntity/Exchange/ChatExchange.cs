namespace DomainEntity.Exchange;

public class ChatExchange
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
