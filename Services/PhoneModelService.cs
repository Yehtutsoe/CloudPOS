using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CloudPOS.Services
{
    public class PhoneModelService : IPhoneModelService
    {
        private readonly IPhoneModelRepository _modelRepository;

        public PhoneModelService(IPhoneModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }
        public void Create(PhoneModelViewModel modelViewModel)
        {
            var entity = new PhoneModelEntity()
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

        public PhoneModelViewModel GetById(string Id)
        {
            var entity = _modelRepository.GetById(Id).FirstOrDefault();
            if(entity == null)
            {
                return null;
            }
            return new PhoneModelViewModel {
                Id = entity.Id,
                Name = entity.Name,
                Brand = entity.Brand,
                Specification =entity.Specification,
                ReleaseDate = entity.ReleaseDate
            };
        }

        public IEnumerable<PhoneModelViewModel> RetrieveAll()
        {
            var entities = _modelRepository.RetrieveAll();
            return entities.Where(w => w.IsActive).Select(s => new PhoneModelViewModel { 
                Id = s.Id,
                Name = s.Name,
                Brand = s.Brand,
                Specification = s.Specification,
                ReleaseDate = s.ReleaseDate
            }).ToList();
        }

        public void Update(PhoneModelViewModel modelViewModel)
        {
            var entities = new PhoneModelEntity()
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
