using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface IPurchaseRepository
    {
        void Create(PurchaseEntity purchaseEntity);
        IEnumerable<PurchaseEntity> RetrieveAll();
        IEnumerable<PurchaseEntity> GetById(string Id);
        void Delete(string Id);
        void Update(PurchaseEntity purchaseEntity);
    }
}
