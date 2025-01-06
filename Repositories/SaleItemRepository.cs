using CloudPOS.DAO;
using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SaleItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(SaleItemEntity saleItemEntity)
        {
            _applicationDbContext.SaleItems.Add(saleItemEntity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
           var existingEntity =  _applicationDbContext.SaleItems.Select(s=> s.Id).FirstOrDefault();
            if (existingEntity != null) {
                _applicationDbContext.Remove(existingEntity);
                _applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<SaleItemEntity> GetById(string Id)
        {
            return _applicationDbContext.SaleItems.Where(s => s.Id == Id).ToList();
        }

        public IEnumerable<SaleItemEntity> RetrieveAll()
        {
            return _applicationDbContext.SaleItems.ToList();
        }

        public void Update(SaleItemEntity saleItemEntity)
        {
            var existingEntity = _applicationDbContext.SaleItems.Find(saleItemEntity.Id);
            if (existingEntity != null) {
                _applicationDbContext.Entry(saleItemEntity).CurrentValues.SetValues(existingEntity);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
