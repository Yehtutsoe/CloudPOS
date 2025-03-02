using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class PurchaseDetailRepository : BaseRepository<PurchaseDetailEntity>, IPurchaseDetailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public void DeleteRange(IEnumerable<PurchaseDetailEntity> purchaseDetails)
        {
           _dbContext.PurchaseDetails.RemoveRange(purchaseDetails);
        }

        public IEnumerable<PurchaseDetailEntity> FindByPurchaseId(string purchaseId)
        {
            return _dbContext.PurchaseDetails.Where(pd => pd.PurchaseId == purchaseId).ToList();
        }
    }
}
