using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class SaleOrderRepository : BaseRepository<SaleEntity>, ISaleOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
