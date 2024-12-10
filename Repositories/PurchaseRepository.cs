using CloudPOS.DAO;
using CloudPOS.Models.Entities;

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
            throw new NotImplementedException();
        }

        public IEnumerable<PurchaseEntity> GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PurchaseEntity> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(PurchaseEntity purchaseEntity)
        {
            throw new NotImplementedException();
        }
    }
}
