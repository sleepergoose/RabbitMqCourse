namespace RabbitMqCourse.Shared.Options;
public class RabbitMQOptions
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = string.Empty;
    public int Port { get; set; }

}
