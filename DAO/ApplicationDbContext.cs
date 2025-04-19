using CloudPOS.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.DAO
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; } 
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }
        public DbSet<SaleItemEntity> SaleItems { get; set; }
        public DbSet<PurchaseEntity> Purchases { get; set; }
        public DbSet<PurchaseDetailEntity> PurchaseDetails { get; set; }
        public DbSet<InventoryEntity> Inventories { get; set; }
        public DbSet<StockLedgerEntity> StockLedgers { get; set; }
        public DbSet<PriceEntity> Prices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision and scale for PurchaseDetailEntity
            modelBuilder.Entity<PurchaseDetailEntity>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

            modelBuilder.Entity<PurchaseDetailEntity>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

            // Configure decimal precision and scale for other entities as needed
            modelBuilder.Entity<PriceEntity>()
                .Property(p => p.PurchasePrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PriceEntity>()
                .Property(p => p.RetailSalePrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PriceEntity>()
                .Property(p => p.WholeSalePrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PurchaseEntity>()
                .Property(p => p.TotalCost)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleEntity>()
                .Property(p => p.Balance)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleEntity>()
                .Property(p => p.CashAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleEntity>()
                .Property(p => p.DiscountAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleEntity>()
                .Property(p => p.DiscountPercent)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleEntity>()
                .Property(p => p.NetTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleEntity>()
                .Property(p => p.SubTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleEntity>()
                .Property(p => p.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleEntity>()
                .Property(p => p.TotalReturnAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleItemEntity>()
                .Property(p => p.DiscountAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleItemEntity>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleItemEntity>()
                .Property(p => p.SaleAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<SaleItemEntity>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18, 2)");
        }

    }
}
