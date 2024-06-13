using RabbitMqCourse.Carts.DAL.Context;
using RabbitMqCourse.Carts.Services;
using RabbitMqCourse.Shared;
using RabbitMqCourse.Shared.Deduplication;

namespace RabbitMqCourse.Carts;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMessaging(builder.Configuration,
            c => c.AddDeduplication<CartsDbContext>(builder.Configuration));
        builder.Services.AddDataAccess(builder.Configuration);
        builder.Services.AddHostedService<MessagingBackgroundService>();
        builder.Services.AddHostedService<AppInitializer>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapGet("/", () => "Carts Service");

        app.Run();
    }
}
