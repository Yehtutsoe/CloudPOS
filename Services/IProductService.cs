using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IProductService
    {
        void Create(ProductViewModel productViewModel);
        IList<ProductViewModel> RetrieveAll();
        ProductViewModel GetById(string Id);
        void Delete(string Id);
        ProductViewModel GetCategoryAndModel();

    }
}
