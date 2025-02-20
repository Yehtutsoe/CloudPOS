using CloudPOS.DAO;
using CloudPOS.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloudPOS.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; }
        public ISaleRepository Sales { get; }
        public ISaleItemRepository SaleItems { get; }
        public IStockIncomeRepository StockIncomes { get; }
        public ICategoryRepository Categories { get; } // ✅ Fixed name (Categories)
        public IBrandRepository Brands { get; }
        public ISupplierRepository Suppliers { get; } // ✅ Fixed to readonly

        public UnitOfWork(ApplicationDbContext context,
                          IProductRepository productRepository,
                          ISaleRepository saleRepository,
                          ISaleItemRepository saleItemRepository,
                          IStockIncomeRepository stockIncomeRepository,
                          ICategoryRepository categoryRepository,
                          IBrandRepository brands,
                          ISupplierRepository suppliers)
        {
            _context = context;
            Products = productRepository;
            Sales = saleRepository;
            SaleItems = saleItemRepository;
            StockIncomes = stockIncomeRepository;
            Categories = categoryRepository; // ✅ Corrected assignment
            Brands = brands;
            Suppliers = suppliers;
        }

        public void Commit()
        {
            _context.SaveChanges(); // ✅ Commit transaction
        }

        public void Dispose()
        {
            _context.Dispose(); // ✅ Dispose context to avoid memory leak
        }

        public void Rollback()
        {
            // ✅ Rollback transaction (Not disposing here)
            _context.Database.CurrentTransaction?.Rollback();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
