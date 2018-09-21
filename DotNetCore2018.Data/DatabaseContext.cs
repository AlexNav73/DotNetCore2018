using System;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore2018.Data
{
    public interface IDatabaseContext
    {
        DbSet<Category> Categories { get; }
    }

    public sealed class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
         : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
