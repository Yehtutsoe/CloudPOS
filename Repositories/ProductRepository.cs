using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;

namespace CloudPOS.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(ProductViewModel productViewModel)
        {
            ProductEntity productEntity = new ProductEntity() { 
                Id = Guid.NewGuid().ToString(),
                Type= productViewModel.Type,
                SerialNumber = productViewModel.SerialNumber,
                IMEINumber = productViewModel.IMEINumber,
                SalePrice = productViewModel.SalePrice,
                CostPrice = productViewModel.CostPrice,
                IsActive = true,
                CategoryId = productViewModel.CategoryId,
                PhoneModelId = productViewModel.PhoneModelId
            };
            _applicationDbContext.Products.Add(productEntity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var existingProduct = _applicationDbContext.Products.Where(w => w.Id == Id).SingleOrDefault();
            if (existingProduct != null) {
                _applicationDbContext.Remove(existingProduct);
                _applicationDbContext.SaveChanges();
            }
        }

        public ProductViewModel GetById(string Id)
        {
            var products = _applicationDbContext.Products.Where(w => w.Id == Id)
                                                         .Select(s => new ProductViewModel
                                                                    {
                                                                        Id = s.Id,
                                                                        SalePrice = s.SalePrice,
                                                                        CostPrice = s.CostPrice,
                                                                        IMEINumber = s.IMEINumber,
                                                                        SerialNumber = s.SerialNumber,
                                                                        CategoryId = s.CategoryId,
                                                                        PhoneModelId = s.PhoneModelId
                                                                    }).SingleOrDefault();

            return products;
        }

        public IList<CategoryViewModel> GetCategories()
        {
            IList<CategoryViewModel> categories = _applicationDbContext.Categorys.Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Name= s.Name + "|" + s.Description
            }).ToList();
            return categories;
        }

        public IList<PhoneModelViewModel> GetPhonesModels()
        {
            IList<PhoneModelViewModel> phoneModels = _applicationDbContext.PhoneModels.Select(s => new PhoneModelViewModel
            {
                Id = s.Id,
                Name = s.Name + "|" + s.Brand
            }).ToList();

            return phoneModels;
        }

        public IList<ProductViewModel> RetrieveAll()
        {
            IList<ProductViewModel> product =(from p in _applicationDbContext.Products
                                                join c in _applicationDbContext.Categorys
                                                on p.CategoryId equals c.Id
                                                join phm in _applicationDbContext.PhoneModels
                                                on p.PhoneModelId equals phm.Id
                                                select new  ProductViewModel
                                                {
                                                    Id = p.Id,
                                                    Type = p.Type,
                                                    CostPrice = p.CostPrice,
                                                    SalePrice = p.SalePrice,
                                                    IMEINumber = p.IMEINumber,
                                                    SerialNumber = p.SerialNumber,
                                                    CategoryId = p.CategoryId,
                                                    PhoneModelId = p.PhoneModelId,
                                                    CategoryInfo = c.Description,
                                                    PhoneModelInfo = phm.Name,

                                                }).ToList();
            return product;
        }
    }
}
