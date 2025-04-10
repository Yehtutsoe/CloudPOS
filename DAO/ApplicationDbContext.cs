﻿using CloudPOS.Models.Entities;
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

    }
}
