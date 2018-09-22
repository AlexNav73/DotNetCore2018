using System;
using Microsoft.EntityFrameworkCore;
using DotNetCore2018.Data.Entities;

namespace DotNetCore2018.Data
{
    public interface IAppContext
    {
        DbSet<Category> Categories { get; }
    }

    public sealed class AppContext : DbContext, IAppContext
    {
        public AppContext(DbContextOptions<AppContext> options)
         : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
