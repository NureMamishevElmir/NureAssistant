using DomainEntity.ChatEntities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class MessageRepository : EntityFrameworkRepository<Message>, IMessageRepository
{
    public MessageRepository(DbContext context) : base(context)
    {
    }
}
