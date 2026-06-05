using AutoMapper;
using DomainEntity.AIEntities;
using DomainEntity.Exchange;
using DomainEntity.Request;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services;

public class AIFileChatService : IAIFileChatService
{
    private readonly IAIFileChatRepository _repository;
    private readonly IMapper _mapper;

    public AIFileChatService(IAIFileChatRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AIFileChatExchange> SetAsync(Guid? id, AIFileChatRequest request, CancellationToken cancellationToken = default)
    {
        AIFileChat entity;
        if (id is null || id == Guid.Empty)
        {
            entity = _mapper.Map<AIFileChat>(request);
            _repository.Add(entity);
        }
        else
        {
            entity = await _repository.GetByIdAsync(id.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"AIFileChat {id} not found.");
            _mapper.Map(request, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            _repository.Update(entity);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AIFileChatExchange>(entity);
    }

    public async Task<AIFileChatExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<AIFileChatExchange>(entity);
    }

    public async Task<List<AIFileChatExchange>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAll().ToListAsync(cancellationToken);
        return _mapper.Map<List<AIFileChatExchange>>(entities);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _repository.Remove(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}
