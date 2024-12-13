using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface ISaleRepository
    {
        void Create(SaleEntity saleEntity);
        IEnumerable<SaleEntity> RetrieveAll();
        IEnumerable<SaleEntity> GetById(string Id);
        void Update(SaleEntity saleEntity);
        void Delete(SaleEntity saleEntity);
        Decimal CalculateProfit(SaleEntity saleEntity);
    }
}
