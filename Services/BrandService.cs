using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;
using CloudPOS.UnitOfWork;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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
            var entity = new BrandEntity()
            {
                Id = brandViewModel.Id,
                Name = brandViewModel.Name,
                Brand = brandViewModel.Brand,
                CreatedAt = DateTime.Now,
                IsActive = true,
                ReleaseDate = brandViewModel.ReleaseDate,
                Specification = brandViewModel.Specification,
                
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
            throw new NotImplementedException();
        }

        public IEnumerable<BrandViewModel> GetBrands()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BrandViewModel> GetBrandsByCategory(string categoryId)
        {
            throw new NotImplementedException();
        }

        public BrandViewModel GetById(string id)
        {
            throw new NotImplementedException();
        }

        public string GetLastBrandCode()
        {
            throw new NotImplementedException();
        }

        public bool IsAlreadyExist(BrandViewModel brandViewModel)
        {
            throw new NotImplementedException();
        }

        public void Update(BrandViewModel brandViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
