using DomainEntity.ChatEntities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class ChatRepository : EntityFrameworkRepository<Chat>, IChatRepository
{
    public ChatRepository(DbContext context) : base(context)
    {
    }
}
