using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface ISaleItemRepository
    {
        Task Create(SaleItemEntity saleItemEntity);
        IEnumerable<SaleItemEntity> RetrieveAll();
        IEnumerable<SaleItemEntity> GetById(string SaleId);
        void Delete(string Id);
        IEnumerable<ProductEntity> GetProducts();
    }
}
