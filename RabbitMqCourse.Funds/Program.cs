
using RabbitMqCourse.Funds.Messages;
using RabbitMqCourse.Shared;

namespace RabbitMqCourse.Funds;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddMessaging(builder.Configuration);

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/", () => "Funds Service");

        app.MapGet("/country/{value}", async (IMessagePublisher messagePublisher, string value) =>
        {
            var message = new FundMessage(123, 10.00m);
            await messagePublisher.PublishAsync("Funds", $"Country.{value}", message);
        });

        app.Run();
    }
}
