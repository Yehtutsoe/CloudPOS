using CloudPOS.DAO;
using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SupplierRepository(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(SupplierEntity supplier)
        {
            _applicationDbContext.Suppliers.Add(supplier);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
           var entity = _applicationDbContext.Suppliers.Find(Id);
            if (entity != null) {
                _applicationDbContext.Suppliers.Remove(entity);
                _applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<SupplierEntity> GetById(string Id)
        {
            return _applicationDbContext.Suppliers.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<SupplierEntity> RetrieveAll()
        {
           return _applicationDbContext.Suppliers.ToList();
        }

        public void Update(SupplierEntity supplier)
        {
            var existingEnity = _applicationDbContext.Suppliers.Find(supplier.Id);
            if(existingEnity != null)
            {
                _applicationDbContext.Entry(existingEnity).CurrentValues.SetValues(supplier);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
