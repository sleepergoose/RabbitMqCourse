using RabbitMqCourse.Carts.Services;
using RabbitMqCourse.Shared;

namespace RabbitMqCourse.Carts;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddMessaging(builder.Configuration);
        builder.Services.AddHostedService<MessagingBackgroundService>();
        builder.Services.AddDataAccess(builder.Configuration);
        builder.Services.AddHostedService<AppInitializer>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/", () => "Carts Service");

        app.Run();
    }
}
