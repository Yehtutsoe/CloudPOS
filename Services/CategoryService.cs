using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(CategoryViewModel categoryViewModel)
        {
            
            CategoryEntity entity = new CategoryEntity()
            {
                Id =Guid.NewGuid().ToString(),
                Name = categoryViewModel.Name,
                Code = categoryViewModel.Code,
                CreatedAt = DateTime.Now,
                Description = categoryViewModel.Description,
                IsActive = true
            };
            _unitOfWork.Categories.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string Id)
        {
            try
            {
                var entity = _unitOfWork.Categories.GetBy(w => w.Id == Id).SingleOrDefault();
                if (entity != null)
                {
                    _unitOfWork.Categories.Delete(entity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {

                throw new Exception("Category not found"+ e.Message);
            }
        }

        public CategoryViewModel GetById(string Id)
        {
            return _unitOfWork.Categories.GetBy(w => w.Id == Id).Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code,
                Description = s.Description    
            }).FirstOrDefault();
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            return _unitOfWork.Categories.GetAll().Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                Name = s.Name
            }).AsEnumerable();
        }

        public void Update(CategoryViewModel categoryViewModel)
        {
            var existingEntity = _unitOfWork.Categories.GetBy(w => w.Id == categoryViewModel.Id).FirstOrDefault();
            if(existingEntity == null)
            {
                throw new Exception("Category not found to update");
            }
            existingEntity.Id = categoryViewModel.Id;
            existingEntity.Code = categoryViewModel.Code;
            existingEntity.Name = categoryViewModel.Name;
            existingEntity.Description = categoryViewModel.Description;
            existingEntity.CreatedAt = DateTime.Now;
            existingEntity.IsActive = true;
            _unitOfWork.Categories.Update(existingEntity);
            _unitOfWork.Commit();
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _unitOfWork.Categories.GetCategorys();
        }

        public bool IsAlreadyExist(CategoryViewModel categoryViewModel)
        {
            return _unitOfWork.Categories.IsAlreadyExist(categoryViewModel.Code, categoryViewModel.Name);
        }

        public string GetNextCategoryCode()
        {
            var lastCode = _unitOfWork.Categories.GetLastCategoryCode();
            if (lastCode != null) 
            {
                int newCode = int.Parse(lastCode) + 1;
                return newCode.ToString("D3");
            }
            else
            {
                return "001";
            }
        }
    }
}
