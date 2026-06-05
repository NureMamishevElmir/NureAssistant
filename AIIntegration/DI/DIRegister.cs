using System.Net.Http.Headers;
using AIIntegration.Configuration;
using AIIntegration.Services;
using AIIntegration.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AIIntegration.DI;

public static class DIRegister
{
    public static IServiceCollection ConfigureAIIntegrationDI(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OpenWebUiSettings>(configuration.GetSection(OpenWebUiSettings.SectionName));

        services.AddHttpClient<IAssistantService, AssistantService>((provider, client) =>
        {
            var settings = provider.GetRequiredService<IOptions<OpenWebUiSettings>>().Value;

            client.BaseAddress = new Uri(settings.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds);

            if (!string.IsNullOrWhiteSpace(settings.ApiKey))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", settings.ApiKey);
            }
        });

        return services;
    }
}
