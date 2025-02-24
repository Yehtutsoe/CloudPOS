using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface ISaleItemRepository : IBaseRepository<SaleItemEntity>
    {
        void Create(IList<SaleItemEntity> saleItemEntity);
        IEnumerable<SaleItemEntity> RetrieveAll();
        IEnumerable<SaleItemEntity> GetById(string SaleId);
        void Delete(string Id);
        IEnumerable<ProductEntity> GetProducts();
    }
}
