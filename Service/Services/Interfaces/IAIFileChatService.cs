using DomainEntity.Exchange;
using DomainEntity.Request;

namespace Service.Services.Interfaces;

public interface IAIFileChatService
{
    Task<AIFileChatExchange> SetAsync(Guid? id, AIFileChatRequest request, CancellationToken cancellationToken = default);

    Task<AIFileChatExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<AIFileChatExchange>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
