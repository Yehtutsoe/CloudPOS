using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleEntity>> GetAllSale();
        Task<SaleEntity> GetById(string id);
        Task Create(SaleProcessViewModel model);
        Task Delete(string Id);
        Task Update(SaleProcessViewModel model);
    }
}
