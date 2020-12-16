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

        public DbSet<Category> category { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Stock> stock { get; set; }
        public DbSet<StockDbContext> stockDbContext { get; set; }
        public DbSet<StockFile> stockFile { get; set; }
        public DbSet<StockProduct> stockProduct { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<CompanyUser> companyUser { get; set; }
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
