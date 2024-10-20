﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Product.Api.Data;

#nullable disable

namespace Product.Services.Api.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240113143247_Products")]
    partial class Products
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Product.Api.Model.Products", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductId");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Category 1",
                            CreatedAt = new DateTime(2024, 1, 13, 19, 32, 46, 927, DateTimeKind.Local).AddTicks(4294),
                            Description = "Description for Product 1",
                            ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Funsplash.com%2Fs%2Fphotos%2Fproducts&psig=AOvVaw3CKSM2sxX786LUDZsD_TgV&ust=1705242395279000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCICwl4nJ2oMDFQAAAAAdAAAAABAJ",
                            Name = "Product 1",
                            Price = 19.99m
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Category 2",
                            CreatedAt = new DateTime(2024, 1, 13, 19, 32, 46, 927, DateTimeKind.Local).AddTicks(4320),
                            Description = "Description for Product 2",
                            ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.pexels.com%2Fsearch%2Fproduct%2F&psig=AOvVaw3CKSM2sxX786LUDZsD_TgV&ust=1705242395279000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCICwl4nJ2oMDFQAAAAAdAAAAABAE",
                            Name = "Product 2",
                            Price = 29.99m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
