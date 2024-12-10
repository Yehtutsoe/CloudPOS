using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IPurchaseService
    {
        void Create(PurchaseViewModel purchaseViewModel);
        IEnumerable<PurchaseViewModel> RetrieveAll();
        void Delete(string Id);
        PurchaseViewModel GetById(string Id);
        void Update(PurchaseViewModel purchaseViewModel);
    }
}
