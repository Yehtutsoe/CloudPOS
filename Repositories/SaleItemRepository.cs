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
            throw new NotImplementedException();
        }

        public IEnumerable<SaleItemEntity> GetById(string SaleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SaleItemEntity> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(SaleItemEntity saleItemEntity)
        {
            throw new NotImplementedException();
        }
    }
}
