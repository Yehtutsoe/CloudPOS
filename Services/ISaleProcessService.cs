using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ISaleProcessService
    {
        Task Create(SaleViewModel SaleViewModel,SaleItemViewModel saleItemViewModel);
        IList<SaleItemViewModel> GetAll();
    }
}
