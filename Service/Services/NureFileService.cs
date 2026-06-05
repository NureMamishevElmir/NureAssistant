using AutoMapper;
using DomainEntity.Exchange;
using DomainEntity.FileEntities;
using DomainEntity.Request;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services;

public class NureFileService : INureFileService
{
    private readonly INureFileRepository _repository;
    private readonly IMapper _mapper;

    public NureFileService(INureFileRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<NureFileExchange> SetAsync(Guid? id, NureFileRequest request, CancellationToken cancellationToken = default)
    {
        NureFile entity;
        if (id is null || id == Guid.Empty)
        {
            entity = _mapper.Map<NureFile>(request);
            _repository.Add(entity);
        }
        else
        {
            entity = await _repository.GetByIdAsync(id.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"NureFile {id} not found.");
            _mapper.Map(request, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            _repository.Update(entity);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<NureFileExchange>(entity);
    }

    public async Task<NureFileExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<NureFileExchange>(entity);
    }

    public async Task<List<NureFileExchange>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAll().ToListAsync(cancellationToken);
        return _mapper.Map<List<NureFileExchange>>(entities);
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
