using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ISaleItemService
    {
        SaleItemViewModel GetActiveProduct();
    }
}
