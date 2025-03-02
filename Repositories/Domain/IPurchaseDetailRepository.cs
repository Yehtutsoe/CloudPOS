using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface IPurchaseDetailRepository:IBaseRepository<PurchaseDetailEntity>
    {
        IEnumerable<PurchaseDetailEntity> FindByPurchaseId(string purchaseId);
        void DeleteRange(IEnumerable<PurchaseDetailEntity> purchaseDetails);
    }
}
