using AutoMapper;
using DomainEntity.AIEntities;
using DomainEntity.Exchange;
using DomainEntity.Request;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services;

public class AIService : IAIService
{
    private readonly IAIRepository _repository;
    private readonly IMapper _mapper;

    public AIService(IAIRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AIExchange> SetAsync(Guid? id, AIRequest request, CancellationToken cancellationToken = default)
    {
        AI entity;
        if (id is null || id == Guid.Empty)
        {
            entity = _mapper.Map<AI>(request);
            _repository.Add(entity);
        }
        else
        {
            entity = await _repository.GetByIdAsync(id.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"AI {id} not found.");
            _mapper.Map(request, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            _repository.Update(entity);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AIExchange>(entity);
    }

    public async Task<AIExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<AIExchange>(entity);
    }

    public async Task<List<AIExchange>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAll().ToListAsync(cancellationToken);
        return _mapper.Map<List<AIExchange>>(entities);
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
