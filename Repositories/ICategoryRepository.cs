using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories
{
    public interface ICategoryRepository: IBaseRepository<CategoryEntity>
    {
        void Create(CategoryEntity categoryEntity);
        IEnumerable<CategoryEntity> RetrieveAll();
        IEnumerable<CategoryEntity> GetById(string Id);
        void Delete(string Id);
        void Update(CategoryEntity categoryEntity);
    }
}
