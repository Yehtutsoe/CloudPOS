using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface IProductRepository : IBaseRepository<ProductEntity>
    {
        IEnumerable<ProductViewModel> GetProducts();
        bool IsAlreadyExist(string productName, string productCode);
        string GetNextProductCode();
        IEnumerable<ProductViewModel> GetProductByCategory(string categoryId);
        ProductEntity FindById(string productId);
    }
}
