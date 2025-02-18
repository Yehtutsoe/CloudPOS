using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories
{
    public interface ISaleRepository:IBaseRepository<SaleEntity>
    {
        IEnumerable<SaleEntity> GetAll();
        SaleEntity GetById(string Id);
        Task Create(SaleEntity entity);
        Task Delete(SaleEntity entity);

    }
}
