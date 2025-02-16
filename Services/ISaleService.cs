using CloudPOS.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudPOS.Services
{
    public interface ISaleService
    {
        Task CreateSale(SaleViewModel saleViewModel, List<SaleItemViewModel> saleItemViewModels);
        void DeleteSale(string saleId);
        IList<SaleItemViewModel> GetAllSales();
    }
}
