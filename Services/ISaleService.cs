using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleProcessViewModel>> GetAllSale();
        Task<SaleProcessViewModel> GetById(string id);
        Task Create(SaleProcessViewModel model);
        Task Delete(string Id);
        Task Update(SaleProcessViewModel model);
    }
}
