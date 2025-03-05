using CloudPOS.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudPOS.Services
{
    public interface ISaleOrderService
    {
        void Create(SaleWithSaleItemViewModel model);
        IEnumerable<SaleOrderViewModel> GetAll(DateTime? fromDate=null,DateTime? toDate=null,string? VoucherNo=null);
    }
}
