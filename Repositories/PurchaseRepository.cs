using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PurchaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(PurchaseEntity purchaseEntity)
        {
            _applicationDbContext.Purchases.Add(purchaseEntity);
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

        public IEnumerable<SupplierEntity> GetActiveSupplier()
        {
            return _applicationDbContext.Suppliers.ToList();
        }

        public IEnumerable<PurchaseEntity> GetById(string Id)
        {
            return _applicationDbContext.Purchases.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<PurchaseEntity> RetrieveAll()
        {
            return _applicationDbContext.Purchases
                                        .Include(s => s.Suppliers)
                                        .ToList();
        }

        public void Update(PurchaseEntity purchaseEntity)
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
