using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface ICategoryRepository
    {
        void Create(CategoryEntity categoryEntity);
        IEnumerable<CategoryEntity> RetrieveAll();
        IEnumerable<CategoryEntity> GetById(string Id);
        void Delete(string Id);
        void Update(CategoryEntity categoryEntity);
    }
}
