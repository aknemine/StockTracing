using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockTracing.DataAccess.DataBaseClasses
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<StockDbContext> stockDbContext { get; set; }
        public DbSet<StockFile> stockFiles { get; set; }
        public DbSet<StockProduct> stockProducts { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<CompanyUser> companyUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CompanyUser>(o =>
            {
                o.HasKey(r => new { r.userId, r.companyId });
            });

            modelBuilder.Entity<StockProduct>(o =>
            {
                o.Property(r => r.id).HasColumnName("productId");
                o.HasKey(r => new { r.stockId, r.id });
            });


        }
    }
}
