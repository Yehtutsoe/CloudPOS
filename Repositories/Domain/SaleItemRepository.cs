using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class SaleItemRepository : BaseRepository<SaleItemEntity>, ISaleItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(IList<SaleItemEntity> saleItemEntity)
        {
            _dbContext.SaleItems.Add((SaleItemEntity)saleItemEntity);
            _dbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var existingEntity = _dbContext.SaleItems.Select(s => s.Id).FirstOrDefault();
            if (existingEntity != null)
            {
                _dbContext.Remove(existingEntity);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<SaleItemEntity> GetById(string Id)
        {
            return _dbContext.SaleItems.Where(s => s.SaleId == Id).ToList();
        }

        public IEnumerable<ProductEntity> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public IEnumerable<SaleItemEntity> RetrieveAll()
        {
            return _dbContext.SaleItems.ToList();
        }
    }
}
