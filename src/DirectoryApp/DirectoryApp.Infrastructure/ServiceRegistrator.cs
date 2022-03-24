using DirectoryApp.Application.Cache;
using DirectoryApp.Application.ConfigurationModel;
using DirectoryApp.Application.MessageSender;
using DirectoryApp.Infrastructure.Cache;
using DirectoryApp.Infrastructure.MessageSender;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DirectoryApp.Infrastructure;

public static class ServiceRegistrator
{
    public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ICache>(serviceProvider =>  //Connect Once, and Use Permanently
        {
            var redisConfig = serviceProvider.GetRequiredService<IOptions<RedisConfiguration>>().Value;
            var redisCache = new RedisCache(redisConfig);
            return redisCache;
        });

        serviceCollection.AddSingleton<IMessageSender>(serviceProvider =>
        {
            var rabbitMQConfig = serviceProvider.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;
            var messageSender = new RabbitMQMessageSender(rabbitMQConfig);

            //İnitializing connection, exchanges and queues
            messageSender.InitializeConnection(rabbitMQConfig);
            messageSender.InitializeExchangesAndQueues(rabbitMQConfig);

            return messageSender;
        });

        serviceCollection.AddSingleton<IMessageSender, RabbitMQMessageSender>();
    }
}