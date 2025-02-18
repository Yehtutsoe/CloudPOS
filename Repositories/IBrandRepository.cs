using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories
{
    public interface IBrandRepository:IBaseRepository<BrandEntity>
    {
        IEnumerable<BrandViewModel> GetBrands();
        bool IsAlreadyExist(string Code,string Name);
        string GetLastBrandCode();
        IEnumerable<BrandViewModel> GetBrandByCategory(string CategoryId);

    }
}
