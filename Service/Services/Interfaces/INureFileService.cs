using DomainEntity.Exchange;
using DomainEntity.Request;

namespace Service.Services.Interfaces;

public interface INureFileService
{
    Task<NureFileExchange> SetAsync(Guid? id, NureFileRequest request, CancellationToken cancellationToken = default);

    Task<NureFileExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<NureFileExchange>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
