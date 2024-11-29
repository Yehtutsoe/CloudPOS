using CloudPOS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.DAO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
          
        }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<PhoneModelEntity> PhoneModels { get; set; }
        public DbSet<CategoryEntity> Categorys { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }
        public DbSet<SaleItemEntity> SaleItems { get; set; }
        public DbSet<PurchaseEntity> Purchases { get; set; }
        public DbSet<PurhcaseItemEntity> PurchaseItems { get; set; }
        public DbSet<InventoryEntity> Inventorys { get; set; }
    }
}
    

