namespace DomainEntity.BaseEnitiy;

public class BaseImmutableEntity : BaseImmutableEntityWithoutId
{
    [DefaultSqlValue("NEWSEQUENTIALID()")]
    public Guid Id { get; }
}
