using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CloudPOS.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _modelRepository;

        public BrandService(IBrandRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }
        public void Create(BrandViewModel modelViewModel)
        {
            var entity = new BrandEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = modelViewModel.Name,
                Brand = modelViewModel.Brand,
                IsActive = true,
                ReleaseDate = modelViewModel.ReleaseDate,
                Specification = modelViewModel.Specification
            };
            _modelRepository.Create(entity);
        }

        public void Delete(string Id)
        {
             _modelRepository.Delete(Id);
        }

        public BrandViewModel GetById(string Id)
        {
            var entity = _modelRepository.GetById(Id).FirstOrDefault();
            if(entity == null)
            {
                return null;
            }
            return new BrandViewModel {
                Id = entity.Id,
                Name = entity.Name,
                Brand = entity.Brand,
                Specification =entity.Specification,
                ReleaseDate = entity.ReleaseDate
            };
        }

        public IEnumerable<BrandViewModel> RetrieveAll()
        {
            var entities = _modelRepository.RetrieveAll();
            return entities.Where(w => w.IsActive).Select(s => new BrandViewModel { 
                Id = s.Id,
                Name = s.Name,
                Brand = s.Brand,
                Specification = s.Specification,
                ReleaseDate = s.ReleaseDate
            }).ToList();
        }

        public void Update(BrandViewModel modelViewModel)
        {
            var entities = new BrandEntity()
            {
                Id = modelViewModel.Id,
                Name = modelViewModel.Name,
                Brand = modelViewModel.Brand,
                Specification = modelViewModel.Specification,
                ReleaseDate = modelViewModel.ReleaseDate
            };
            entities.IsActive = true;
            _modelRepository.Update(entities);
        }
    }
}
