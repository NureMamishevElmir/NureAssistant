namespace DomainEntity.BaseEnitiy;

public class BaseImmutableEntityWithoutId
{
    [DefaultSqlValue("GETDATE()")]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public void AdjustCreationDate(TimeSpan timespan)
    {
        CreatedAt = CreatedAt.Add(timespan);
    }
}
