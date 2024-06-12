using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RabbitMqCourse.Carts.DAL.Config;
using RabbitMqCourse.Carts.DAL.Models;

namespace RabbitMqCourse.Carts.DAL.Context;

public class CartsDbContext : DbContext
{
    public DbSet<CustomerFundsModel> CustomerFunds { get; set; }

    public CartsDbContext(DbContextOptions<CartsDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SuperStore.Carts");

        var configuration = new GetConfiguration();
        modelBuilder.ApplyConfiguration<CustomerFundsModel>(configuration);
    }
}
