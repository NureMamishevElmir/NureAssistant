using DomainEntity.Exchange;
using DomainEntity.Request;

namespace AIIntegration.Services;

public interface IAssistantService
{
    Task<MessageExchange> AskAsync(MessageRequest request, CancellationToken cancellationToken = default);

    Task<bool> IsAvailableAsync(CancellationToken cancellationToken = default);
}
