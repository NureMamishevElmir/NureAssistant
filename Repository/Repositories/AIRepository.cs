using DomainEntity.AIEntities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class AIRepository : EntityFrameworkRepository<AI>, IAIRepository
{
    public AIRepository(DbContext context) : base(context)
    {
    }
}
