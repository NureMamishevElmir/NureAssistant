using AutoMapper;
using DomainEntity.CustomerEntities;
using DomainEntity.Exchange;
using DomainEntity.Request;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerExchange> SetAsync(Guid? id, CustomerRequest request, CancellationToken cancellationToken = default)
    {
        Customer entity;
        if (id is null || id == Guid.Empty)
        {
            entity = _mapper.Map<Customer>(request);
            _repository.Add(entity);
        }
        else
        {
            entity = await _repository.GetByIdAsync(id.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"Customer {id} not found.");
            _mapper.Map(request, entity);
            entity.UpdatedAt = DateTime.UtcNow;
            _repository.Update(entity);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CustomerExchange>(entity);
    }

    public async Task<CustomerExchange?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<CustomerExchange>(entity);
    }

    public async Task<List<CustomerExchange>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAll().ToListAsync(cancellationToken);
        return _mapper.Map<List<CustomerExchange>>(entities);
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
