using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RabbitMqCourse.Carts.DAL.Context;

internal sealed class CartsDbContextFactory : IDesignTimeDbContextFactory<CartsDbContext>
{
    public CartsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CartsDbContext>();
        optionsBuilder.UseNpgsql(string.Empty);

        return new CartsDbContext(optionsBuilder.Options);
    }
}
