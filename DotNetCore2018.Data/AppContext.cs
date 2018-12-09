using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DotNetCore2018.Data.Entities;

namespace DotNetCore2018.Data
{
    public sealed class AppContext : IdentityDbContext<User, UserRole, Guid>
    {
        public AppContext(DbContextOptions<AppContext> options)
         : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
