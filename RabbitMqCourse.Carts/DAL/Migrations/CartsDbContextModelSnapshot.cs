﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RabbitMqCourse.Carts.DAL.Context;

#nullable disable

namespace RabbitMqCourse.Carts.Migrations
{
    [DbContext(typeof(CartsDbContext))]
    partial class CartsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("SuperStore.Carts")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RabbitMqCourse.Carts.DAL.Models.CustomerFundsModel", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("CustomerId"));

                    b.Property<decimal>("CurrentFunds")
                        .HasColumnType("numeric");

                    b.HasKey("CustomerId");

                    b.ToTable("CustomerFunds", "SuperStore.Carts");
                });

            modelBuilder.Entity("RabbitMqCourse.Shared.Deduplication.DeduplicationModel", b =>
                {
                    b.Property<string>("MessageId")
                        .HasColumnType("text");

                    b.Property<DateTime>("ProcessedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("MessageId");

                    b.ToTable("Deduplications", "SuperStore.Carts");
                });
#pragma warning restore 612, 618
        }
    }
}
