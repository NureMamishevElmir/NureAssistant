using DomainEntity.Exchange;
using DomainEntity.Request;

namespace Identity.Services;

public interface IAuthService
{
    Task<(string Token, DateTime ValidTo, CustomerExchange Customer)> RegisterAsync(CustomerRequest request, CancellationToken cancellationToken = default);

    Task<(string Token, DateTime ValidTo, CustomerExchange Customer)> LoginAsync(CustomerRequest request, CancellationToken cancellationToken = default);
}
