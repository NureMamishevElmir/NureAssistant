namespace DomainEntity.Request;

public class ChatRequest
{
    public string? Name { get; set; }

    public Guid CustomerId { get; set; }
}
