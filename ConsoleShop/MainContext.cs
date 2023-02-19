using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsoleShop.Models;

namespace ConsoleShop
{
    public class MainContext : DbContext
    {
        private readonly string _connectionString = "Data Source=WIN-M6AALBI6UOR;Initial Catalog=WebShopNiktosBigPisos;Integrated Security=True";
        public DbSet<Client> Client { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }

        public MainContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(n => n.Id).IsUnique();
                entity.HasData(new List<Client>()
                {
                    new Client() {Id = 1, Name = "Саня", Surname = "Бодров", Address = "4-ая Железнодорожная", Phone = "+79378286840"},
                    new Client() {Id = 2, Name = "Даник", Surname = "Пудиков", Address = "Савушкина 37", Phone = "+79378281328"},
                    new Client() {Id = 3, Name = "Илья", Surname = "Дубин", Address = "Чикатилово 80", Phone = "+79276548976"},
                    new Client() {Id = 4, Name = "Анна", Surname = "Птицына", Address = "Загородный 70", Phone = "+79675667454"},
                    new Client() {Id = 5, Name = "Юлия", Surname = "Руднева", Address = "Комсомольская 9", Phone = "+79565643544"},
                });
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(n => n.Id).IsUnique();
                entity.HasData(new List<Product>()
                {
                    new Product() {Id = 1, Name = "Морковь", Description = "Оранжевая", Category = "Овощь", Cost = 24 , Count = 80},
                    new Product() {Id = 2, Name = "Молоко", Description = "Белое", Category = "Молочка", Cost = 40, Count = 30},
                    new Product() {Id = 3, Name = "Говядина", Description = "Сырая", Category = "Мясо", Cost = 80, Count = 5},
                    new Product() {Id = 4, Name = "Огурец", Description = "Оранжевая", Category = "Овощь", Cost = 24 , Count = 80},
                    new Product() {Id = 5, Name = "Сметана", Description = "Белое", Category = "Молочка", Cost = 40, Count = 30},
                    new Product() {Id = 6, Name = "Курятина", Description = "Сырая", Category = "Мясо", Cost = 80, Count = 5},
                });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasData(new List<Order>()
                {
                    new Order() {Id = 1, DateOfCreation = DateTime.Now, DateOfUpdate = DateTime.Now, Status = "Сформирован", Delivery = "Самокат", Payment = "Карта", Cost = 90, ClientId = 1, ProductId = 1},
                    new Order() {Id = 2, DateOfCreation = DateTime.Now, DateOfUpdate = DateTime.Now, Status = "Оплачен", Delivery = "Машина", Payment = "Карта", Cost = 20, ClientId = 2, ProductId = 2},
                    new Order() {Id = 3, DateOfCreation = DateTime.Now, DateOfUpdate = DateTime.Now, Status = "Отправлен", Delivery = "Велосипед", Payment = "Наличкой", Cost = 190, ClientId = 3, ProductId = 3},
                    new Order() {Id = 4, DateOfCreation = DateTime.Now, DateOfUpdate = DateTime.Now, Status = "Доставлен", Delivery = "Пеший", Payment = "Карта", Cost = 290, ClientId = 4, ProductId = 4},
                    new Order() {Id = 5, DateOfCreation = DateTime.Now, DateOfUpdate = DateTime.Now, Status = "Сформирован", Delivery = "Самокат", Payment = "Карта", Cost = 90, ClientId = 5, ProductId = 5},
                });
            });
        }
    }
}
