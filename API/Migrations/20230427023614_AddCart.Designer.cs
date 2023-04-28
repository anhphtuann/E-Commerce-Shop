﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20230427023614_AddCart")]
    partial class AddCart
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Carts.Cart", b =>
                {
                    b.Property<int>("cartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cartId"));

                    b.Property<decimal>("totalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("cartId");

                    b.HasIndex("userId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("API.Models.Carts.CartProduct", b =>
                {
                    b.Property<int>("cartId")
                        .HasColumnType("int");

                    b.Property<int>("proId")
                        .HasColumnType("int");

                    b.Property<decimal>("priceUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("quantity")
                        .HasColumnType("bigint");

                    b.Property<decimal>("totalProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("cartId", "proId");

                    b.HasIndex("proId");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("API.Models.Products.Category", b =>
                {
                    b.Property<int>("cateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cateId"));

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cateId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("API.Models.Products.Product", b =>
                {
                    b.Property<int>("proId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("proId"));

                    b.Property<string>("brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cateId")
                        .HasColumnType("int");

                    b.Property<string>("productDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("proId");

                    b.HasIndex("cateId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("API.Models.Products.Stock", b =>
                {
                    b.Property<int>("cateId")
                        .HasColumnType("int");

                    b.Property<int>("proId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateModify")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("priceUnit")
                        .HasColumnType("money");

                    b.Property<long>("quantity")
                        .HasColumnType("bigint");

                    b.Property<int>("vendorUserId")
                        .HasColumnType("int");

                    b.HasKey("cateId", "proId");

                    b.HasIndex("proId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("API.Models.Reviews.Review", b =>
                {
                    b.Property<int>("reviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("reviewId"));

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("proId")
                        .HasColumnType("int");

                    b.Property<double>("rate")
                        .HasColumnType("float");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("reviewId");

                    b.HasIndex("proId");

                    b.HasIndex("userId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("API.Models.Users.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<string>("account")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("lastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.Carts.Cart", b =>
                {
                    b.HasOne("API.Models.Users.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("API.Models.Carts.CartProduct", b =>
                {
                    b.HasOne("API.Models.Carts.Cart", "cart")
                        .WithMany("products")
                        .HasForeignKey("cartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Products.Product", "product")
                        .WithMany()
                        .HasForeignKey("proId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cart");

                    b.Navigation("product");
                });

            modelBuilder.Entity("API.Models.Products.Product", b =>
                {
                    b.HasOne("API.Models.Products.Category", "category")
                        .WithMany("products")
                        .HasForeignKey("cateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("API.Models.Products.Stock", b =>
                {
                    b.HasOne("API.Models.Products.Category", "category")
                        .WithMany()
                        .HasForeignKey("cateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Products.Product", "product")
                        .WithMany()
                        .HasForeignKey("proId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("product");
                });

            modelBuilder.Entity("API.Models.Reviews.Review", b =>
                {
                    b.HasOne("API.Models.Products.Product", "product")
                        .WithMany("reviews")
                        .HasForeignKey("proId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Users.User", "user")
                        .WithMany("reviews")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("user");
                });

            modelBuilder.Entity("API.Models.Carts.Cart", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("API.Models.Products.Category", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("API.Models.Products.Product", b =>
                {
                    b.Navigation("reviews");
                });

            modelBuilder.Entity("API.Models.Users.User", b =>
                {
                    b.Navigation("reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
