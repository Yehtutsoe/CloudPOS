﻿using CloudPOS.DAO;
using CloudPOS.Repositories.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloudPOS.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; }
        public ISaleOrderRepository Sales { get; }
        public ISaleItemRepository SaleItems { get; }
        public ICategoryRepository Categories { get; } 
        public IBrandRepository Brands { get; }
        public ISupplierRepository Suppliers { get; } 
        public IInventoryRepository Inventories { get; }
        public IPurchaseRepository Purchases { get; }
        public IPurchaseDetailRepository PurchaseDetails { get; }
        public IStockledgerRepository StockLedgers { get; }
        public IPriceRepository Prices { get; }

        public UnitOfWork(ApplicationDbContext context,
                          IProductRepository productRepository,
                          ISaleOrderRepository saleRepository,
                          ISaleItemRepository saleItemRepository,
                          ICategoryRepository categoryRepository,
                          IBrandRepository brands,
                          ISupplierRepository suppliers,
                          IInventoryRepository inventories,
                          IPurchaseDetailRepository purchaseDetails,
                          IPurchaseRepository purchases,
                          IStockledgerRepository stockledger,
                          IPriceRepository prices
                          )
        {
            _context = context;
            Products = productRepository;
            Sales = saleRepository;
            SaleItems = saleItemRepository;
            Categories = categoryRepository;
            Brands = brands;
            Suppliers = suppliers;
            Inventories = inventories;
            PurchaseDetails = purchaseDetails;
            Purchases = purchases;
            StockLedgers = stockledger;
            Prices = prices;
        }

        public void Commit()
        {
            _context.SaveChanges(); //  Commit transaction
        }

        public void Dispose()
        {
            _context.Dispose(); //  Dispose context to avoid memory leak
        }

        public void Rollback()
        {
            //  Rollback transaction (Not disposing here)
            _context.Database.CurrentTransaction?.Rollback();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
