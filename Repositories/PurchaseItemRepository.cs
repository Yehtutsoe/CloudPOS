using CloudPOS.DAO;
using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public class PurchaseItemRepository : IPurchaseItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PurchaseItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(PurchaseItemEntity purchaseItemEntitty)
        {
            _applicationDbContext.Add(purchaseItemEntitty);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            
        }

        public IEnumerable<PurchaseItemEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PurchaseItemEntity> GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public void Update(PurchaseItemEntity purchaseItemEntitty)
        {
            throw new NotImplementedException();
        }
    }
}
