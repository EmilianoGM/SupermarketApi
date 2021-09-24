using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupermarketApi.Entities;
using System;

namespace SupermarketApi.DataAccess
{
    /*
     * Must inherit DbContext, a class EF Core uses to map models to database tables.
    */
    public class SupermarketDbContext : IdentityDbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProducts> Orderproducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        /*
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        /*
         * The constructor is responsible for passing the database configuration to the base class through dependency injection.
         */
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.Property(e => e.OrderId).IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Address).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderProducts>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PRIMARY");

                entity.ToTable("orderproducts");
                
                entity.HasIndex(e => e.ProductId, "FK_ProductId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
                entity.HasIndex(e => e.CategoryId, "Fk_CategoryProduct");

                entity.Property(e => e.ImgUrl).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(30);
                entity.Property(p => p.UnitOfMeasurement).HasColumnType("enum('Unity','Milligram','Gram','Kilogram','Liter')").HasConversion(v => v.ToString(), v => (EUnitOfMeasurement)Enum.Parse(typeof(EUnitOfMeasurement), v));
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("Fk_CategoryProduct");
            });
            /*
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
            builder.Entity<Product>().Property(p => p.ImgUrl).HasMaxLength(150);
            builder.Entity<Product>().Property(p => p.Price);
            builder.Entity<Product>().HasKey(p => p.Id);
            
            builder.Entity<Order>().ToTable("orders");
            builder.Entity<Order>().Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Order>().Property(o => o.Address).HasMaxLength(50);
            builder.Entity<Order>().Property(o => o.TotalPrice);
            builder.Entity<Order>().HasKey(o => o.Id);

            builder.Entity<OrderProducts>().HasKey(op => new { op.OrderId, op.ProductId}  );
            builder.Entity<OrderProducts>()
                .HasOne<Order>(op => op.Order)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            builder.Entity<OrderProducts>().HasKey(op => new { op.OrderId, op.ProductId });
            builder.Entity<OrderProducts>()
                .HasOne<Product>(op => op.Product)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(op => op.ProductId);*/
        }
    }
}
