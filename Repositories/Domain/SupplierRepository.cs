using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class SupplierRepository : BaseRepository<SupplierEntity> , ISupplierRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplierRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public SupplierEntity FindById(string Id)
        {
            return _dbContext.Suppliers.FirstOrDefault(s => s.Id == Id);
        }

        public IEnumerable<SupplierViewModel> GetSupplier()
        {
            return _dbContext.Suppliers.Select(s => new SupplierViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public bool IsAlreadyExist(string code, string name)
        {
            return _dbContext.Suppliers.Where(s => s.Code != code && s.Name != name).Any();
        }

        public string GetNextSupplierCode()
        {
            var lastSupplier = _dbContext.Suppliers
                  .OrderByDescending(s => s.Code)
                  .FirstOrDefault();
            return lastSupplier != null ? lastSupplier.Code : null;
        }
    }
}
