using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;


namespace CloudPOS.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(BrandViewModel brandViewModel)
        {
            BrandEntity entity = new BrandEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = brandViewModel.Name,
                CreatedAt = DateTime.Now,
                IsActive = true,
                Code = brandViewModel.Code,
                CategoryId = brandViewModel.CategoryId   
            };
            _unitOfWork.Brands.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string id)
        {
            try
            {
                var existingEntity = _unitOfWork.Brands.GetBy(b => b.Id == id).SingleOrDefault();
                if (existingEntity != null)
                {
                    _unitOfWork.Brands.Delete(existingEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {

                throw new Exception("Brand not found" + e.Message);
            }
        }

        public IEnumerable<BrandViewModel> GetAll()
        {
            var brands = (from b in _unitOfWork.Brands.GetAll().ToList()
                          join c in _unitOfWork.Categories.GetAll().ToList()
                          on b.CategoryId equals c.Id
                          select new BrandViewModel 
                          { 
                             Id = b.Id,
                             CategoryId = b.Id,
                             Name = b.Name,
                             Code = b.Code,
                             CategoryName = c.Name

                          }).ToList();
            return brands;
        }

        public IEnumerable<BrandViewModel> GetBrands()
        {
            return _unitOfWork.Brands.GetBrands();
        }

        public IEnumerable<BrandViewModel> GetBrandsByCategory(string categoryId)
        {
            return _unitOfWork.Brands.GetBrandByCategory(categoryId);
        }

        public BrandViewModel GetById(string id)
        {
            return _unitOfWork.Brands.GetBy(w => w.Id == id).Select(s => new BrandViewModel
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                Code = s.Code,
                Name = s.Name
                
            }).FirstOrDefault();
        }

        public string GetLastBrandCode()
        {
            var lastCode = _unitOfWork.Brands.GetLastBrandCode();
            
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

        public bool IsAlreadyExist(BrandViewModel brandViewModel)
        {
            return _unitOfWork.Brands.IsAlreadyExist(brandViewModel.Code,brandViewModel.Name);
        }

        public void Update(BrandViewModel brandViewModel)
        {
            var existingEntity = _unitOfWork.Brands.GetBy(w => w.Id ==  brandViewModel.Id).FirstOrDefault();
            if(existingEntity == null)
            {
                throw new Exception("Brand not found to update");
            }
            existingEntity.Name = brandViewModel.Name;
            existingEntity.CategoryId = brandViewModel.CategoryId;

            _unitOfWork.Brands.Update(existingEntity);
            _unitOfWork.Commit();
        }
    }
}
