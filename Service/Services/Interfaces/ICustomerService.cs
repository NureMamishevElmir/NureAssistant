using DomainEntity.Exchange;
using DomainEntity.Request;

namespace Service.Services.Interfaces;

public interface ICustomerService
{
    Task<CustomerExchange> SetAsync(Guid? id, CustomerRequest request, CancellationToken cancellationToken = default);

    Task<CustomerExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<CustomerExchange>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
