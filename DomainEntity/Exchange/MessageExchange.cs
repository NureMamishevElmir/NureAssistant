namespace DomainEntity.Exchange;

public class MessageExchange
{
    public Guid Id { get; set; }

    public string? Text { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ChatId { get; set; }

    public Guid FileId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
