using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace CloudPOS.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SaleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Create(SaleEntity entity,SaleItemEntity saleItemEntity)
        {
           _applicationDbContext.Sales.AddAsync(entity);
            _applicationDbContext.SaleItems.AddAsync(saleItemEntity);
            _applicationDbContext.SaveChangesAsync();   
        }

        public async Task Delete(string Id)
        {
            var sale = await _applicationDbContext.Sales.FindAsync(Id);
            if (sale != null) {
                _applicationDbContext.Sales.Remove(sale);
                _applicationDbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<SaleEntity>> GetAllSaleAsync()
        {
            return await _applicationDbContext.Sales.Include(s => s.SaleItems).ThenInclude(si =>si.Products).ToListAsync();
        }

        public async Task<SaleEntity> GetById(string Id)
        {
            return await _applicationDbContext.Sales.Include(s => s.SaleItems).SingleOrDefaultAsync(s => s.Id == Id);
        }

        public async Task Update(SaleEntity entity)
        {
            _applicationDbContext.Sales.Update(entity);
            _applicationDbContext.SaveChangesAsync();
        }
    }
}
