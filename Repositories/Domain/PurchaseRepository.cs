using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class PurchaseRepository : BaseRepository<PurchaseEntity>, IPurchaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public PurchaseEntity FindById(string Id)
        {
            return _dbContext.Purchases.FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<PurchaseViewModel> GetPurchase()
        {
            return _dbContext.Purchases.Select(p => new PurchaseViewModel()
            {
               Id = p.Id,
               VoucherNo = p.VoucherNo
            }).ToList();
        }
    }
}
