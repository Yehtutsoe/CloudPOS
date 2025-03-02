using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IPurchaseService
    {
        void Create(PurchaseWithDetailViewModel models);
        IEnumerable<PurchaseViewModel> GetAll(DateTime? fromDate = null,DateTime? toDate = null,string? supplierId = null,string? purchaseId = null);
        bool Delete(string Id);
        IEnumerable<PurchaseViewModel> GetPurchase();
    }
}
