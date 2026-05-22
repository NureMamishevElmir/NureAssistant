using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;

namespace Repository.DI;

public static class DIRegister
{
    public static void ConfigureRepositoryDI(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}
