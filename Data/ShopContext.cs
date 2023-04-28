using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Shop.Data
{
    public class ShopContext: DbContext
    {
        private readonly IConfiguration _configure;

        public ShopContext(DbContextOptions<ShopContext> options, IConfiguration configure): base(options){
            _configure = configure;
        }
        public DbSet<Products> Product => Set<Products>();
        public DbSet<Category> Category => Set<Category>();
        public DbSet<Review> Review => Set<Review>();
        public DbSet<User> User => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
        }
         protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configure.GetConnectionString("DefaultConnection"));           

        }
    }
}