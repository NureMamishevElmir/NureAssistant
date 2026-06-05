using DomainEntity.AIEntities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class AIFileRepository : EntityFrameworkRepository<AIFile>, IAIFileRepository
{
    public AIFileRepository(DbContext context) : base(context)
    {
    }
}
