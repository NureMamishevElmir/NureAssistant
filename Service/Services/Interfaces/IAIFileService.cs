using DomainEntity.Exchange;
using DomainEntity.Request;

namespace Service.Services.Interfaces;

public interface IAIFileService
{
    Task<AIFileExchange> SetAsync(Guid? id, AIFileRequest request, CancellationToken cancellationToken = default);

    Task<AIFileExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<AIFileExchange>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
