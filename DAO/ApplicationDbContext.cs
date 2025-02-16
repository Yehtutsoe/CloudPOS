using CloudPOS.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.DAO
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<BrandEntity> PhoneModels { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }
        public DbSet<SaleItemEntity> SaleItems { get; set; }
        public DbSet<StockIncomeEntity> StockIncomes { get; set; }
        public DbSet<InventoryEntity> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // use the identity framework(not use custom User entity)
            modelBuilder.Entity<SaleEntity>()
                .HasMany(s => s.SaleItems)
                .WithOne(si => si.Sales)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete if necessary
        }

    }
}
    

