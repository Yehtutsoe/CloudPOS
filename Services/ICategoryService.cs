using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ICategoryService
    {
        void Create(CategoryViewModel categoryViewModel);
        IEnumerable<CategoryViewModel> RetrieveAll();
        CategoryViewModel GetById(string Id);
        void Delete(string Id);
        void Update(CategoryViewModel categoryViewModel);
    }
}
