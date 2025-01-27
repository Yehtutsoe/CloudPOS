using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface IInventoryRepository
    {
        void Create(InventoryEntity inventoryEntity);
        void Update(InventoryEntity inventoryEntity);
        IEnumerable<InventoryEntity> RetrieveAll();
        IEnumerable<InventoryEntity> GetById(string Id);
        void Delete(string Id);
    }
}
