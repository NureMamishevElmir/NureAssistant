using DomainEntity.Exchange;
using DomainEntity.Request;

namespace Service.Services.Interfaces;

public interface IChatService
{
    Task<ChatExchange> SetAsync(Guid? id, ChatRequest request, CancellationToken cancellationToken = default);

    Task<ChatExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<ChatExchange>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<int> DeleteStaleAsync(int olderThanDays, CancellationToken cancellationToken = default);

    Task<int> DeleteAllAsync(CancellationToken cancellationToken = default);
}
