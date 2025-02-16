using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SaleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Create(SaleEntity entity)
        {
          await _applicationDbContext.Sales.AddAsync(entity);
           await _applicationDbContext.SaveChangesAsync();   
        }

        public async Task Delete(SaleEntity entity)
        {
            _applicationDbContext.Sales.Remove(entity);
           await _applicationDbContext.SaveChangesAsync();
        }

        public IEnumerable<SaleEntity> GetAll()
        {
            return _applicationDbContext.Sales.ToList();
        }

        public SaleEntity GetById(string Id)
        {
            return  _applicationDbContext.Sales
                                              .Include(s => s.SaleItems)
                                              .ThenInclude(si => si.Product)
                                              .SingleOrDefault(s => s.Id == Id);
        }

    }
}
