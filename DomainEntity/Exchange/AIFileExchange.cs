namespace DomainEntity.Exchange;

public class AIFileExchange
{
    public Guid Id { get; set; }

    public Guid AIId { get; set; }

    public Guid FileId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
