using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void Create(ProductViewModel productViewModel)
        {
            _productRepository.Create(productViewModel);
        }

        public void Delete(string Id)
        {
            _productRepository.Delete(Id);
        }

        public ProductViewModel GetById(string Id)
        {
            var product = _productRepository.GetById(Id);
            if(product == null)
            {
                return null;
            }
            product.CategoryViewModels = _productRepository.GetCategories();
            product.PhoneModelViewModels = _productRepository.GetPhonesModels();
            return product;
        }

        public ProductViewModel GetCategoryAndModel()
        {
            var products = new ProductViewModel()
            {
                CategoryViewModels = _productRepository.GetCategories(),
                PhoneModelViewModels = _productRepository.GetPhonesModels()
            };
            return products;
        }

        public IList<ProductViewModel> RetrieveAll()
        {
            return _productRepository.RetrieveAll();
        }

        public void Update(ProductViewModel productViewModel)
        {
            var entity = new ProductEntity()
            {
                Id = productViewModel.Id,
                Type = productViewModel.Type,
                CategoryId = productViewModel.CategoryId,
                PhoneModelId = productViewModel.PhoneModelId,
                IMEINumber = productViewModel.IMEINumber,
                SerialNumber = productViewModel.SerialNumber,
                CostPrice = productViewModel.CostPrice,
                SalePrice = productViewModel.SalePrice                
            };
            entity.IsActive = true;
            _productRepository.Update(entity);
        }
    }
}
