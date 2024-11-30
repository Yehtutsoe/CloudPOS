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
            return _productRepository.GetById(Id);
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
    }
}
