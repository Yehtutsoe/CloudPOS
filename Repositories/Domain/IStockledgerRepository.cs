using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface IStockledgerRepository:IBaseRepository<StockLedgerEntity>
    {
        IEnumerable<StockLedgerEntity> FindById(string Id);
        void DeleteRange(IEnumerable<StockLedgerEntity> entity);
    }
}
