using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Domain;
using Humanizer;

namespace CloudPOS.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public void Create(SupplierViewModel supplierViewModel)
        {
            SupplierEntity entity = new SupplierEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = supplierViewModel.Name,
                Address = supplierViewModel.Address,
                ContactInformation = supplierViewModel.ContactInformation,
                IsActive = true
            };
            _supplierRepository.Create(entity);
        }

        public void Delete(string Id)
        {
            _supplierRepository.Delete(Id);
        }

        public SupplierViewModel GetById(string Id)
        {
            var entity = _supplierRepository.GetById(Id).SingleOrDefault();
            if(entity == null)
            {
                return null;
            }
            return new SupplierViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                ContactInformation = entity.ContactInformation,
                Address = entity.Address
            };
        }

        public IEnumerable<SupplierViewModel> RetrieveAll()
        {
            var entity = _supplierRepository.RetrieveAll();
            return entity.Select(s => new SupplierViewModel {
                Id = s.Id,
                Name = s.Name,
                ContactInformation = s.ContactInformation,
                Address =s.Address
            }).ToList();
        }

        public void Update(SupplierViewModel supplierViewModel)
        {
            SupplierEntity entity = new SupplierEntity()
            {
                Id = supplierViewModel.Id,
                Name = supplierViewModel.Name,
                ContactInformation = supplierViewModel.ContactInformation,
                Address = supplierViewModel.Address
            };
            entity.IsActive = true;
            _supplierRepository.Update(entity);
        }
    }
}
