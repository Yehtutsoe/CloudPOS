using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface ISaleItemRepository
    {
        void Create(SaleItemEntity saleItemEntity);
        IEnumerable<SaleItemEntity> RetrieveAll();
        IEnumerable<SaleItemEntity> GetById(string SaleId);
        void Delete(string Id);
        void Update(SaleItemEntity saleItemEntity);
    }
}
