using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface IInventoryRepository : IBaseRepository<InventoryEntity>
    {
        InventoryEntity GetInventoryByProductAndEarliest(string productId,DateTime earliestDate);
        void UpdateInventoryBalanceByProductAndEarliest(string productId, int quantity,string categoryId,DateTime earliestDate);
        List<(DateTime EarliestDate, int QuantityUsed)> ReduceForSale(string productId, int quantity);
        int GetAvaliableStock(string productId);

    }
}
