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

        public void Delete(string Id)
        {
            var entity = _applicationDbContext.StockIncomes.Find(Id);
            if(entity != null)
            {
                _applicationDbContext.StockIncomes.Remove(entity);
                _applicationDbContext.SaveChanges();
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
            return _applicationDbContext.StockIncomes.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<StockIncomeEntity> RetrieveAll()
        {
            return _applicationDbContext.StockIncomes
                                        .Include(s => s.Suppliers)
                                        .Include(p => p.Products)
                                        .ToList();
        }

        public void Update(StockIncomeEntity stockEntity)
        {
           var existingEntity = _applicationDbContext.StockIncomes.Find(stockEntity.Id);
            if(existingEntity != null)
            {
                _applicationDbContext.Entry(existingEntity).CurrentValues.SetValues(stockEntity);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
