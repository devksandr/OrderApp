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
            modelBuilder.Entity<Provider>().HasData(
                new Provider { Id = 1, Name = "Intel" },
                new Provider { Id = 2, Name = "Nvidia" },
                new Provider { Id = 3, Name = "Dell" },
                new Provider { Id = 4, Name = "Asus" },
                new Provider { Id = 5, Name = "Acer" },
                new Provider { Id = 6, Name = "Huawei" },
                new Provider { Id = 7, Name = "Lenovo" },
                new Provider { Id = 8, Name = "Xiaomi" },
                new Provider { Id = 9, Name = "BenQ" },
                new Provider { Id = 10, Name = "HP" }
            );

            // test - del
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, Number = "ASDJK-ASDAS-ASDAS", Date = DateTime.Now, ProviderId = 1 },
                new Order { Id = 2, Number = "FDGDF-3FSDS-BXCXV", Date = DateTime.Now, ProviderId = 7 }
            );
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, Name = "CPU1", Quantity = 1, Unit = "Unit1.1" },
                new OrderItem { Id = 2, OrderId = 1, Name = "CPU2", Quantity = 1, Unit = "Unit1.2" },
                new OrderItem { Id = 3, OrderId = 2, Name = "Phone1", Quantity = 2, Unit = "Unit2" }
            );
        }
    }
}
