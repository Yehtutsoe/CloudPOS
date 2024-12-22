using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SaleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Create(SaleEntity saleEntity)
        {
            _applicationDbContext.Sales.Add(saleEntity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(SaleEntity saleEntity)
        {
            _applicationDbContext.Sales.Remove(saleEntity);
            _applicationDbContext.SaveChanges();
        }

        public SaleEntity GetById(string Id)
        {
            return _applicationDbContext.Sales.Include(s => s.SaleItems).SingleOrDefault(s => s.Id == Id);
        }

        public IEnumerable<SaleEntity> RetrieveAll()
        {
            return _applicationDbContext.Sales.Include(s => s.SaleItems).ToList();
        }

        public void Update(SaleEntity saleEntity)
        {
            var existingEntity = _applicationDbContext.Sales.Find(saleEntity.Id);
            if (existingEntity != null) { 
                _applicationDbContext.Entry(existingEntity).CurrentValues.SetValues(saleEntity);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
