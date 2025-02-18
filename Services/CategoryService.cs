using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void Create(CategoryViewModel categoryViewModel)
        {
            var entity = new CategoryEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description,
                IsActive = true
            };
            _categoryRepository.Create(entity);
        }

        public void Delete(string Id)
        {
            _categoryRepository.Delete(Id);
        }

        public CategoryViewModel GetById(string Id)
        {
            var entity = _categoryRepository.GetById(Id).SingleOrDefault();
            if(entity == null)
            {
                return null;
            }

        public IEnumerable<CategoryViewModel> RetrieveAll()
        {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            }).ToList();
        }

        public void Update(CategoryViewModel categoryViewModel)
        {
            var entity = new CategoryEntity()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description
            };
            entity.IsActive = true;
            _categoryRepository.Update(entity);
        }
    }
}
