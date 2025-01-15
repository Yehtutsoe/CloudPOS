using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface IPurchaseItemRepository
    {
        void Create(PurchaseItemEntity purchaseItemEntitty);
        IEnumerable<PurchaseItemEntity> GetAll();
        IEnumerable<PurchaseItemEntity> GetById(string Id);
        void Delete(string Id);
        void Update(PurchaseItemEntity purchaseItemEntitty);
        public IEnumerable<ProductEntity> GetActiveProduct();
    }
}
