using RabbitMQ.Client;

namespace RabbitMqCourse.Shared.Connections;

internal sealed class ChannelFactory : IChannelFactory
{
    private readonly IConnection _connection;
    private readonly ChannelAccessor _channelAccessor;

    public ChannelFactory(ChannelAccessor channelAccessor, IConnection connection)
    {
        _channelAccessor = channelAccessor;
        _connection = connection;
    }

    public IModel Create()
        => _channelAccessor.Channnel ??= _connection.CreateModel();
}

