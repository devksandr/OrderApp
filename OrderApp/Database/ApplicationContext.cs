using Microsoft.EntityFrameworkCore;
using OrderApp.Models.Entities;

namespace OrderApp.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Provider> Providers { get; set; } = null!;

        string dbPath = null!;
        string dbName = null!;

        public ApplicationContext()
        {
            dbName = "orderapp.db";
            dbPath = System.IO.Path.Join("Database", dbName);

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
