
using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IProductService
    {
        void Create(ProductViewModel productViewModel,PriceViewModel priceViewModel);
        IEnumerable<ProductViewModel> GetAll();
        ProductViewModel GetById(string Id);
        void Delete(string Id);
        IEnumerable<ProductViewModel> GetProducts();
        bool IsAlreadyExist(ProductViewModel productViewModel);
        string GetNextProductCode();
        IEnumerable<ProductViewModel> GetProductByCategory(string categoryId);
        void Update(ProductViewModel productViewModel);
    }
}
