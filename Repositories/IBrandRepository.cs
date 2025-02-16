using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface IBrandRepository
    {
        void Create(BrandEntity entity);
        IEnumerable<BrandEntity> GetById(string Id);
        IEnumerable<BrandEntity> RetrieveAll();
        void Delete(string Id);
        void Update(BrandEntity entity);
    }
}
