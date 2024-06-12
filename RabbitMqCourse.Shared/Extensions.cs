using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMqCourse.Shared.Connections;
using RabbitMqCourse.Shared.Dispatchers;
using RabbitMqCourse.Shared.Options;
using RabbitMqCourse.Shared.Publishers;
using RabbitMqCourse.Shared.Subscribers;

namespace RabbitMqCourse.Shared;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<RabbitMQOptions>("RabbitMQ");

        var factory = new ConnectionFactory
        {
            HostName = options.HostName,
            Port = options.Port,
            Password = options.Password,
            UserName = options.UserName,
            VirtualHost = options.VirtualHost,
        };

        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<ChannelAccessor>();
        services.AddSingleton<IChannelFactory, ChannelFactory>();
        services.AddSingleton<IMessagePublisher, MessagePublisher>();
        services.AddSingleton<IMessageSubscriber, MessageSubscriber>();
        services.AddSingleton<IMessageDispatcher, MessageDispatcher>();

        services.Scan(cfg => cfg.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IMessageHandler<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }

    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
        where TOptions : new()
    {
        var options = new TOptions();
        configuration.GetSection(sectionName).Bind(options);

        return options;

    }
}
