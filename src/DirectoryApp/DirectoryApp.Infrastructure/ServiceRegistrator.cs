using DirectoryApp.Application.Cache;
using DirectoryApp.Infrastructure.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryApp.Infrastructure;

public static class ServiceRegistrator
{
    public static void RegisterPersistenceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ICache, RedisCache>();
    }
}