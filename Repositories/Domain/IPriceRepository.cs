using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface IPriceRepository:IBaseRepository<PriceEntity>
    {
        IEnumerable<PriceViewModel> GetPrices();
        ProductPriceViewModel GetProductDetailsByBarCode(string barCode);
    }
}
