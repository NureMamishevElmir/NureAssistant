using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;

namespace Repository.DI;

public static class DIRegister
{
    public static void ConfigureRepositoryDI(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IAIRepository, AIRepository>();
        services.AddScoped<IAIFileRepository, AIFileRepository>();
        services.AddScoped<IAIFileChatRepository, AIFileChatRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<INureFileRepository, NureFileRepository>();
    }
}
