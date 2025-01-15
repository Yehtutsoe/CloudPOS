using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface IStockIncomeRepository
    {
        void Create(StockIncomeEntity purchaseEntity);
        IEnumerable<StockIncomeEntity> RetrieveAll();
        IEnumerable<StockIncomeEntity> GetById(string Id);
        void Delete(string Id);
        void Update(StockIncomeEntity purchaseEntity);
        IEnumerable<SupplierEntity> GetActiveSupplier();

    }
}
