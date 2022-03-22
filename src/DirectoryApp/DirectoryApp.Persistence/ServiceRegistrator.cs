using DirectoryApp.Application.Repository;
using DirectoryApp.Persistence.Context;
using DirectoryApp.Persistence.EntityFramework.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryApp.Persistence;

public static class ServiceRegistrator
{
    public static void RegisterPersistenceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
        serviceCollection.AddScoped<IEnrolleeRepository, EnrolleeRepository>();
        serviceCollection.AddScoped<IContactInformationRepository, ContactInformationRepository>();
    }
}