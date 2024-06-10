using RabbitMQ.Client;

namespace RabbitMqCourse.Shared;

public interface IChannelFactory
{
    IModel Create();
}
