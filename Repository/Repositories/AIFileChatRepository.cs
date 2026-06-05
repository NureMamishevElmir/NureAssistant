using DomainEntity.AIEntities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class AIFileChatRepository : EntityFrameworkRepository<AIFileChat>, IAIFileChatRepository
{
    public AIFileChatRepository(DbContext context) : base(context)
    {
    }
}
