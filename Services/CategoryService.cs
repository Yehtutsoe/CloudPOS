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
            return new CategoryViewModel() { 
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
        }

        public IEnumerable<CategoryViewModel> RetrieveAll()
        {
            var entity = _categoryRepository.RetrieveAll();

            return entity.Where(w =>w.IsActive).Select(s => new CategoryViewModel { 
                
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
