using DomainEntity.CustomerEntities;

namespace Identity.Services;

public interface ITokenService
{
    (string Token, DateTime ValidTo) Generate(Customer customer);
}
