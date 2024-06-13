using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMqCourse.Carts.DAL.Models;

namespace RabbitMqCourse.Carts.DAL.Config;

internal sealed class GetConfiguration : IEntityTypeConfiguration<CustomerFundsModel>
{
    public void Configure(EntityTypeBuilder<CustomerFundsModel> builder)
    {
        builder.ToTable("CustomerFunds");

        builder.HasKey(x => x.CustomerId);
    }
}
