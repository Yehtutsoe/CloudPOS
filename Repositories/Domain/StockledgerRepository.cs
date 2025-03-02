using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class StockledgerRepository : BaseRepository<StockLedgerEntity>, IStockledgerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockledgerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public void DeleteRange(IEnumerable<StockLedgerEntity> entity)
        {
            _dbContext.StockLedgers.RemoveRange(entity);
        }

        public IEnumerable<StockLedgerEntity> FindById(string Id)
        {
            return _dbContext.StockLedgers.Where(st => st.Id == Id).ToList();
        }
    }
}
