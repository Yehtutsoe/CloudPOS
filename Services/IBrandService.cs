using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IBrandService
    {
        void Create(BrandViewModel brandViewModel);
        IEnumerable<BrandViewModel> GetAll();
        BrandViewModel GetById(string id);
        void Update(BrandViewModel brandViewModel);
        void Delete(string id);
        IEnumerable<BrandViewModel> GetBrands();
        bool IsAlreadyExist(BrandViewModel brandViewModel);
        string GetLastBrandCode();
        IEnumerable<BrandViewModel> GetBrandsByCategory(string categoryId);
    }
}
