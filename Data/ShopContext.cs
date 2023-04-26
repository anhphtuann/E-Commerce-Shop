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
        public ShopContext(DbContextOptions<ShopContext> options): base(options){

        }
        public DbSet<Products> Product => Set<Products>();
        public DbSet<Category> Category => Set<Category>();
        public DbSet<Review> Review => Set<Review>();
        public DbSet<User> User => Set<User>();
    }
}