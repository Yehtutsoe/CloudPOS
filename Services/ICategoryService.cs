using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ICategoryService
    {
        void Create(CategoryViewModel categoryViewModel);
        IEnumerable<CategoryViewModel> GetAll();
        CategoryViewModel GetById(string Id);
        void Delete(string Id);
        void Update(CategoryViewModel categoryViewModel);
        IEnumerable<CategoryViewModel> GetCategories();
        bool IsAlreadyExist(CategoryViewModel categoryViewModel);
        string GetNextCategoryCode();
    }
}
