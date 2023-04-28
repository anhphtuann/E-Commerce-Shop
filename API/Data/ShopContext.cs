using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using API.Models.Products;
using API.Models.Users;
using API.Models.Reviews;
using API.Models.Carts;

namespace API.Data;

public class ShopContext : DbContext
{

    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products {get; set;}
    public DbSet<Category> Categories {get; set;}
    public  DbSet<Stock> Stocks {get; set;}
    public  DbSet<Review> Reviews {get; set;}
    public  DbSet<User> Users {get; set;}
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder dbBuilder)
    {
        base.OnConfiguring(dbBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>()
            .HasKey(s => new {s.cateId, s.proId});
        
        modelBuilder.Entity<CartProduct>()
            .HasKey(c => new {c.cartId, c.proId});
    } 
}
