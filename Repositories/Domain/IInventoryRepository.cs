using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface IInventoryRepository : IBaseRepository<InventoryEntity>
    {
        InventoryEntity GetInventoryByProduct(string productId);
        void UpdateInventoryBalanceByProduct(string productId, string categoryId, int quantity);
        bool ReduceForSale(string productId, int quantity);
        int GetAvaliableStock(string productId);

    }
}
