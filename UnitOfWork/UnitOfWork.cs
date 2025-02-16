using CloudPOS.DAO;
using CloudPOS.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloudPOS.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository Products { get; }
        public ISaleRepository Sales { get; }
        public ISaleItemRepository SaleItems { get; }
        public IStockIncomeRepository StockIncomes { get; }
        public ICategoryRepository Categorys { get; }

        public IBrandRepository Brands { get; }

        public UnitOfWork(ApplicationDbContext context,
                          IProductRepository productRepository,
                          ISaleRepository saleRepository,
                          ISaleItemRepository saleItemRepository,
                          IStockIncomeRepository stockIncomeRepository,
                          ICategoryRepository categoryRepository,
                          IBrandRepository brands)
        {
            _context = context;
            Products = productRepository;
            Sales = saleRepository;
            SaleItems = saleItemRepository;
            StockIncomes = stockIncomeRepository;
            Categorys = categoryRepository;
            Brands = brands;
        }

        public int Commit()
        {
            return _context.SaveChanges(); // Transaction ကို Commit လုပ်မယ်
        }

        public void Dispose()
        {
            _context.Dispose(); // Memory Leak မဖြစ်အောင် Dispose
        }
    }
}
