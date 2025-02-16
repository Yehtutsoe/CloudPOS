using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories
{
    public interface IBrandRepository:IBaseRepository<BrandEntity>
    {
        void Create(BrandEntity entity);
        IEnumerable<BrandEntity> GetById(string Id);
        IEnumerable<BrandEntity> RetrieveAll();
        void Delete(string Id);
        void Update(BrandEntity entity);
    }
}
