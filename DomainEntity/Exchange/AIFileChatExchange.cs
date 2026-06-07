namespace DomainEntity.Exchange;

public class AIFileChatExchange
{
    public Guid Id { get; set; }

    public Guid ChatId { get; set; }

    public Guid FileId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
