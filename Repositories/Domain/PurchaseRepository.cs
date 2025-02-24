using CloudPOS.DAO;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class PurchaseRepository : BaseRepository<PurchaseRepository>, IPurchaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
