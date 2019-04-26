
using Microsoft.EntityFrameworkCore;
using MigrationWithMySql.Models;
using MigrationWithMySql.SettingManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigrationWithMySql.DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {

        }
        public MyDbContext(DbContextOptions<MyDbContext> settings)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(SettingsManager.DatabaseSettings.GetConnectionString());
            }
            base.OnConfiguring(optionsBuilder);
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderDetail>()
              .HasKey(p => new { p.OrderID, p.ProductID });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
    }
}
