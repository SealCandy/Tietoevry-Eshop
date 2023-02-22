using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Models
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext>options)
            : base(options)
        {

        }
        public ShopDbContext()
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasOne(x => x.ParentCategory)
                    .WithMany(x => x.SubCategories)
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                
            });
            modelBuilder.Entity<ProductVariant>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<OrderLine>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,4)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
