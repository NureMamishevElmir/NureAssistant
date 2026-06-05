using AutoMapper;
using DomainEntity.ChatEntities;
using DomainEntity.Exchange;
using DomainEntity.Request;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _repository;
    private readonly IMapper _mapper;

    public ChatService(IChatRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ChatExchange> SetAsync(Guid? id, ChatRequest request, CancellationToken cancellationToken = default)
    {
        Chat entity;
        if (id is null || id == Guid.Empty)
        {
            entity = _mapper.Map<Chat>(request);
            _repository.Add(entity);
        }
        else
        {
            entity = await _repository.GetByIdAsync(id.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"Chat {id} not found.");
            _mapper.Map(request, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            _repository.Update(entity);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ChatExchange>(entity);
    }

    public async Task<ChatExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ChatExchange>(entity);
    }

    public async Task<List<ChatExchange>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAll().ToListAsync(cancellationToken);
        return _mapper.Map<List<ChatExchange>>(entities);
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

    public async Task<int> DeleteStaleAsync(int olderThanDays, CancellationToken cancellationToken = default)
    {
        var cutoff = DateTime.UtcNow.AddDays(-olderThanDays);
        var stale = await _repository.GetAll()
            .Where(c => c.CreatedAt < cutoff)
            .ToListAsync(cancellationToken);

        foreach (var chat in stale)
        {
            _repository.Remove(chat);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return stale.Count;
    }

    public async Task<int> DeleteAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await _repository.GetAll().ToListAsync(cancellationToken);

        foreach (var chat in all)
        {
            _repository.Remove(chat);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return all.Count;
    }
}
