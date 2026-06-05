using DomainEntity.FileEntities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class NureFileRepository : EntityFrameworkRepository<NureFile>, INureFileRepository
{
    public NureFileRepository(DbContext context) : base(context)
    {
    }
}
