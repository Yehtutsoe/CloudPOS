using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface ISaleRepository
    {
        IEnumerable<SaleEntity> GetAll();
        SaleEntity GetById(string Id);
        Task Create(SaleEntity entity);
        Task Delete(SaleEntity entity);

    }
}
