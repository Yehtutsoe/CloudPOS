using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.Repositories
{
    public class SaleRepository : BaseRepository<SaleEntity>, ISaleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(SaleEntity entity)
        {
          await _dbContext.Sales.AddAsync(entity);
           await _dbContext.SaveChangesAsync();   
        }

        public async Task Delete(SaleEntity entity)
        {
            _dbContext.Sales.Remove(entity);
           await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<SaleEntity> GetAll()
        {
            return _dbContext.Sales.ToList();
        }

        public SaleEntity GetById(string Id)
        {
            return  _dbContext.Sales
                                              .Include(s => s.SaleItems)
                                              .ThenInclude(si => si.Product)
                                              .SingleOrDefault(s => s.Id == Id);
        }

    }
}
