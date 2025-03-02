using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface IPurchaseRepository : IBaseRepository<PurchaseEntity>
    {
        IEnumerable<PurchaseViewModel> GetPurchase();
        PurchaseEntity FindById(string Id);
    }
}
