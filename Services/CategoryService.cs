using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;
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
            var entity = new CategoryEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = categoryViewModel.Name,
                Code = categoryViewModel.Code,
                CreatedAt = DateTime.Now,
                Description = categoryViewModel.Description,
                IsActive = true
            };
            _unitOfWork.Categorys.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string Id)
        {
            try
            {
                var entity = _unitOfWork.Categorys.GetBy(w => w.Id == Id).SingleOrDefault();
                if (entity != null)
                {
                    _unitOfWork.Categorys.Delete(entity);
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
            return _unitOfWork.Categorys.GetBy(w => w.Id == Id).Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code,
                Description = s.Description,
                
            }).FirstOrDefault();
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            return _unitOfWork.Categorys.GetAll().Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description,
                Name = s.Name
            }).AsEnumerable();
        }

        public void Update(CategoryViewModel categoryViewModel)
        {
            var entity = new CategoryEntity()
            {
                Id = categoryViewModel.Id,
                Code = categoryViewModel.Code,
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description
            };
            entity.IsActive = true;
            _unitOfWork.Categorys.Update(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _unitOfWork.Categorys.GetCategorys();
        }

        public bool IsAlreadyExist(CategoryViewModel categoryViewModel)
        {
            return _unitOfWork.Categorys.IsAlreadyExist(categoryViewModel.Code, categoryViewModel.Name);
        }

        public string GetNextCategoryCode()
        {
            var lastCode = _unitOfWork.Categorys.GetLastCategoryCode();
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
