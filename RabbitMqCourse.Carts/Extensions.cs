using Microsoft.EntityFrameworkCore;
using RabbitMqCourse.Carts.DAL.Context;
using RabbitMqCourse.Carts.DAL.Options;
using RabbitMqCourse.Shared;

namespace RabbitMqCourse.Carts;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres")
            ?? throw new Exception("There is no the ConnectionString");
        
        var assembly = typeof(CartsDbContext).Assembly.GetName().Name;

        services.AddDbContext<CartsDbContext>(options =>
            options.UseNpgsql(postgresOptions.ConnectionString,
                opts => opts.MigrationsAssembly(assembly)));

        AppContext.SetSwitch(switchName: "Npgsql.EnableLegacyTimestampBehavior", isEnabled: true);

        return services;
    }
}
