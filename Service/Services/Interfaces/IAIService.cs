using DomainEntity.Exchange;
using DomainEntity.Request;

namespace Service.Services.Interfaces;

public interface IAIService
{
    Task<AIExchange> SetAsync(Guid? id, AIRequest request, CancellationToken cancellationToken = default);

    Task<AIExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<AIExchange>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
