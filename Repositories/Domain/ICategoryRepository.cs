using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface ICategoryRepository : IBaseRepository<CategoryEntity>
    {
        IEnumerable<CategoryViewModel> GetCategorys();
        bool IsAlreadyExist(string Code, string Name);
        string GetLastCategoryCode();

    }
}
