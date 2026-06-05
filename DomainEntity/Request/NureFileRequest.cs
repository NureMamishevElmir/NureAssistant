namespace DomainEntity.Request;

public class NureFileRequest
{
    public string? Name { get; set; }

    public string? Weight { get; set; }

    public bool IsActive { get; set; }

    public DateTime ActivatedAt { get; set; }

    public DateTime DeactivatedAt { get; set; }

    public Guid CustomerId { get; set; }
}
