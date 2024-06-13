using Microsoft.Extensions.DependencyInjection;

namespace RabbitMqCourse.Shared;

public interface IMessagingConfiguration
{
    IServiceCollection Services { get; }
}


internal record class MessagingConfiguration(IServiceCollection Services) : IMessagingConfiguration;