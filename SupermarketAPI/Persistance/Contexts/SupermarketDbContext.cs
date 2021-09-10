using Microsoft.EntityFrameworkCore;
using SupermarketAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SupermarketAPI.Persistance.Contexts
{
    /*
     * Must inherit DbContext, a class EF Core uses to map models to database tables.
    */
    public class SupermarketDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        /*
         * The constructor is responsible for passing the database configuration to the base class through dependency injection.
         */
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().ToTable("categories");
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId).HasConstraintName("FK_CategoryProduct");

            builder.Entity<Product>().ToTable("products");
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).HasMaxLength(30);
            builder.Entity<Product>().Property(p => p.QuantityInPackage);
            builder.Entity<Product>().Property(p => p.UnitOfMeasurement).HasConversion(v => v.ToString(), v => (EUnitOfMeasurement)Enum.Parse(typeof(EUnitOfMeasurement), v));
            builder.Entity<Product>().HasKey(p => p.Id);
        }
    }
}
