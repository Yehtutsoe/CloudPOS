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
           _applicationDbContext.Sales.AddAsync(entity);
            _applicationDbContext.SaveChangesAsync();   
        }

        public async Task Delete(string Id)
        {
            var sale = await GetById(Id);
            if (sale == null) {
                throw new KeyNotFoundException("Sale Not Found");
            }
            _applicationDbContext.Sales.Remove(sale);
            _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SaleEntity>> GetAllSaleAsync()
        {
            return await _applicationDbContext.Sales.Include(s => s.SaleItems).ThenInclude(si =>si.Products).ToListAsync();
        }

        public async Task<SaleEntity> GetById(string Id)
        {
            return await _applicationDbContext.Sales
                                              .Include(s => s.SaleItems)
                                              .ThenInclude(si => si.Products)
                                              .SingleOrDefaultAsync(s => s.Id == Id);
        }

    }
}
