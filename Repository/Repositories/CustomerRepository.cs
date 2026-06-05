using DomainEntity.CustomerEntities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class CustomerRepository : EntityFrameworkRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(DbContext context) : base(context)
    {
    }
}
