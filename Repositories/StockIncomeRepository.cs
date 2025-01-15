using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.Repositories
{
    public class StockIncomeRepository : IStockIncomeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StockIncomeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(StockIncomeEntity stockIncome)
        {
            _applicationDbContext.Purchases.Add(stockIncome);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var entity = _applicationDbContext.Purchases.Find(Id);
            if(entity != null)
            {
                _applicationDbContext.Purchases.Remove(entity);
                _applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<ProductEntity> GetActiveProducts()
        {
            return _applicationDbContext.Products.ToList();
        }

        public IEnumerable<SupplierEntity> GetActiveSupplier()
        {
            return _applicationDbContext.Suppliers.ToList();
        }

        public IEnumerable<StockIncomeEntity> GetById(string Id)
        {
            return _applicationDbContext.Purchases.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<StockIncomeEntity> RetrieveAll()
        {
            return _applicationDbContext.Purchases
                                        .Include(s => s.Suppliers)
                                        .ToList();
        }

        public void Update(StockIncomeEntity purchaseEntity)
        {
           var existingEntity = _applicationDbContext.Purchases.Find(purchaseEntity.Id);
            if(existingEntity != null)
            {
                _applicationDbContext.Entry(existingEntity).CurrentValues.SetValues(purchaseEntity);
                _applicationDbContext.SaveChanges();
            }
        }

    }
}
