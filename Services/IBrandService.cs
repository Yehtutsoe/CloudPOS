using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IBrandService
    {
        void Create(BrandViewModel modelViewModel);
        IEnumerable<BrandViewModel> RetrieveAll();
        BrandViewModel GetById(string Id);
        void Update(BrandViewModel modelViewModel);
        void Delete(string Id);
    }
}
