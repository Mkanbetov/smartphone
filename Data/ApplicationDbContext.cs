using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smartphone.Models;

namespace Smartphone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Пример начальных данных для брендов
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Apple" },
                new Brand { Id = 2, Name = "Samsung" },
                new Brand { Id = 3, Name = "Xiaomi" }
            );

            // Пример начальных данных для категорий
            modelBuilder.Entity<Category>().HasData(
                 new Category { Id=1, Name = "Смартфоны" },
                 new Category { Id=2, Name = "Аксессуары" },
                 new Category { Id=3, Name = "Чехлы" },
                 new Category {Id=4, Name = "Зарядные устройства" },
                 new Category {Id=5, Name = "Наушники" },
                 new Category {Id=6, Name = "Защитные стекла" },
                 new Category {Id=7, Name = "Смарт-часы" }
            );

            // Пример начальных данных для продуктов
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 6,
                    Name = "iPhone 14",
                    Price = 999.99M,
                    BrandId = 1,
                    CategoryId = 1,
                    Memory = 128,
                    Battery = 3279,
                    ImageUrl = "/images/iphone14.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S23",
                    Price = 899.99M,
                    BrandId = 2,
                    CategoryId = 1,
                    Memory = 256,
                    Battery = 3900,
                    ImageUrl="/images/s23.jpeg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Xiaomi Redmi Note 12",
                    Price = 299.99M,
                    BrandId = 3,
                    CategoryId = 1,
                    Memory = 256,
                    Battery = 3900,
                    ImageUrl = "/images/xiomi.jpg"
                }
            );
        }
    }
}
