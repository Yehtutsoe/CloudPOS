using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;

namespace CloudPOS.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        #region Constructor
        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        #region Create
        public void Create(ProductViewModel productViewModel)
        {
            ProductEntity productEntity = new ProductEntity() { 
                Id = Guid.NewGuid().ToString(),
                Name= productViewModel.Name,
                SerialNumber = productViewModel.SerialNumber,
                IMEINumber = productViewModel.IMEINumber,
                SalePrice = productViewModel.SalePrice,
                CostPrice = productViewModel.CostPrice,
                IsActive = true,
                Quantity = 1,
                CategoryId = productViewModel.CategoryId,
                PhoneModelId = productViewModel.PhoneModelId,
                
            };
            _applicationDbContext.Products.Add(productEntity);
            _applicationDbContext.SaveChanges();
        }

        #region Delete
        public void Delete(string Id)
        {
            var existingProduct = _applicationDbContext.Products.Where(w => w.Id == Id).SingleOrDefault();
            if (existingProduct != null) {
                _applicationDbContext.Remove(existingProduct);
                _applicationDbContext.SaveChanges();
            }
        }

        #region GetById
        public ProductViewModel GetById(string Id)
        {
            var products = _applicationDbContext.Products.Where(w => w.Id == Id)
                                                         .Select(s => new ProductViewModel
                                                                    {
                                                                        Id = s.Id,
                                                                        Name=s.Name,
                                                                        SalePrice = s.SalePrice,
                                                                        CostPrice = s.CostPrice,
                                                                        IMEINumber = s.IMEINumber,
                                                                        SerialNumber = s.SerialNumber,
                                                                        CategoryId = s.CategoryId,
                                                                        PhoneModelId = s.PhoneModelId
                                                                    }).SingleOrDefault();

            return products;
        }
    }
}
