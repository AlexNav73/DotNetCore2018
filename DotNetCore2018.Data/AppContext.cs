using System;
using Microsoft.EntityFrameworkCore;
using DotNetCore2018.Data.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace DotNetCore2018.Data
{
    public interface IAppContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Supplier> Suppliers { get; }
        DbSet<Product> Products { get; }
        DbSet<User> Users { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

    public sealed class AppContext : DbContext, IAppContext
    {
        public AppContext(DbContextOptions<AppContext> options)
         : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
