using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PD211.Entities;

namespace ShopMvcApp_PD211.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuperPowerShopMvcDb_PD211;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new[]
            {
                new Category() { Id = 1, Name = "Electronics" },
                new Category() { Id = 2, Name = "Sport" },
                new Category() { Id = 3, Name = "Fashion" },
                new Category() { Id = 4, Name = "Home & Garden" },
                new Category() { Id = 5, Name = "Transport" },
                new Category() { Id = 6, Name = "Toys & Hobbies" },
                new Category() { Id = 7, Name = "Musical Instruments" },
                new Category() { Id = 8, Name = "Art" },
                new Category() { Id = 9, Name = "Other" }
            });

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product() { Id = 1, Title = "iPhone X", CategoryId = 1, Discount = 10, Price = 650, Quantity = 5 },

                new Product() { Id = 2, Title = "PowerBall", CategoryId = 2, Price = 45.5M, Quantity = 3 },

                new Product() { Id = 3, Title = "Nike T-Shirt", CategoryId = 3, Discount = 15, Price = 189, Quantity = 3 },

                new Product() { Id = 4, Title = "Samsung S23", CategoryId = 1, Discount = 5, Price = 1200, Quantity = 0 },

                new Product() { Id = 5, Title = "Air Ball", CategoryId = 6, Price = 50, Quantity = 0 },

                new Product() { Id = 6, Title = "MacBook Pro 2019", CategoryId = 1, Discount = 10, Quantity = 23 },

                new Product() { Id = 7, Title = "Samsung S4", CategoryId = 2, Discount = 0, Price = 440, Quantity = 0 },
            });
        }
    }
}
