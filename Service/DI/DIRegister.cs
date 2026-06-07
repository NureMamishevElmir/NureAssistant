using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Service.Automapper;
using Service.Services;
using Service.Services.Interfaces;

namespace Service.DI;

public static class DIRegister
{
    public static IServiceCollection ConfigureServiceDI(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IAIService, AIService>();
        services.AddScoped<IAIFileChatService, AIFileChatService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<INureFileService, NureFileService>();

        services
            .AddSingleton<Profile, AutoMapperServiceProfile>()
            .AddAutoMapper((sp, conf) => conf.AddProfile(sp.GetService<Profile>()), Array.Empty<Assembly>());

        return services;
    }
}
