
using RabbitMqCourse.Funds.Messages;
using RabbitMqCourse.Shared;

namespace RabbitMqCourse.Funds;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddMessaging();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/", () => "Funds Service");

        app.MapGet("/message/send", async (IMessagePublisher messagePublisher) =>
        {
            var message = new FundMessage(123, 10.00m);
            await messagePublisher.PublishAsync("Funds", "FundsMessage", message);
        });

        app.Run();
    }
}
