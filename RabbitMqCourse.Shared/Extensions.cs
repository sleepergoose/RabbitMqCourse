using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMqCourse.Shared.Connections;
using RabbitMqCourse.Shared.Publishers;

namespace RabbitMqCourse.Shared;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Password = "",
            UserName = "",
        };

        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<ChannelAccessor>();
        services.AddSingleton<IChannelFactory, ChannelFactory>();
        services.AddSingleton<IMessagePublisher, MessagePublisher>();

        return services;
    }
}
