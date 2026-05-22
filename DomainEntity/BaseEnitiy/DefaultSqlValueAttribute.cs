namespace DomainEntity.BaseEnitiy;
[AttributeUsage(AttributeTargets.Property)]
public sealed class DefaultSqlValueAttribute : Attribute
{
    public string SqlValue { get; }

    public DefaultSqlValueAttribute(string sqlValue)
    {
        SqlValue = sqlValue;
    }
}
