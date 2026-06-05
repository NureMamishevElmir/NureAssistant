using DomainEntity.Exchange;
using DomainEntity.Request;

namespace Service.Services.Interfaces;

public interface IMessageService
{
    Task<MessageExchange> SetAsync(Guid? id, MessageRequest request, CancellationToken cancellationToken = default);

    Task<MessageExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<MessageExchange>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
