using DomainEntity.CustomerEntities;

namespace Identity.Services;

/// <summary>Генерація JWT-токенів для користувачів.</summary>
public interface ITokenService
{
    /// <summary>Видає підписаний JWT для вказаного користувача.</summary>
    (string Token, DateTime ValidTo) Generate(Customer customer);
}
