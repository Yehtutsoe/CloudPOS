using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IInventoryService
    {
        void CreateOrUpdate(InventoryViewModel inventoryViewModel);
        IEnumerable<InventoryViewModel> GetAll(string productId,string categoryId);
    }
}
