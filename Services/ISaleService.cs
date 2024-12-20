using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ISaleService
    {
        void Create(SaleViewModel model);
        IEnumerable<SaleViewModel> RetrieveAll();
        SaleViewModel GetById(string Id);
        void Delete(string Id);
        void Update(SaleViewModel model);
    }
}
