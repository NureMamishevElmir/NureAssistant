namespace DomainEntity.Request;

public class MessageRequest
{
    public string? Text { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ChatId { get; set; }

    public Guid FileId { get; set; }
}
