using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductEntity FindById(string productId)
        {
            return _dbContext.Products.FirstOrDefault(product => product.Id == productId);
        }

        public string GetNextProductCode()
        {
            var lastProduct = _dbContext.Products.OrderByDescending(product => product.Code).FirstOrDefault();
            return lastProduct != null ? lastProduct.Code : null;
        }

        public IEnumerable<ProductViewModel> GetProductByCategory(string categoryId)
        {
            return _dbContext.Products.Where(p => p.CategoryId == categoryId).Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            return _dbContext.Products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public bool IsAlreadyExist(string productName, string productCode)
        {
            return _dbContext.Products.Where(w => w.Code != productCode && w.Name == productName).Any();
        }
    }
}
