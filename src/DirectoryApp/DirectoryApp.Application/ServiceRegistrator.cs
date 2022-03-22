using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryApp.Application;

public static class ServiceRegistrator
{
    public static void RegisterPersistenceServices(this IServiceCollection serviceCollection)
    {
        var assm = Assembly.GetExecutingAssembly();

        serviceCollection.AddAutoMapper(assm);
        serviceCollection.AddMediatR(assm);
    }
}