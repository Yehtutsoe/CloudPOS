using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(SupplierViewModel supplierViewModel)
        {
            SupplierEntity entity = new SupplierEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Code = supplierViewModel.Code,
                Name = supplierViewModel.Name,
                Address = supplierViewModel.Address,
                ContactInformation = supplierViewModel.ContactInformation,
                CreatedAt = DateTime.Now,
                IsActive = true
            };
            _unitOfWork.Suppliers.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string Id)
        {
            var entity = _unitOfWork.Suppliers.GetBy(s => s.Id == Id).SingleOrDefault();
            _unitOfWork.Suppliers.Delete(entity);
        }

        public SupplierViewModel GetById(string Id)
        {
            return _unitOfWork.Suppliers.GetBy(s => s.Id == Id)
                                              .Select(s => new SupplierViewModel() 
                                              {
                                                  Id = s.Id,
                                                  Code = s.Code,
                                                  Name = s.Name,
                                                  ContactInformation = s.ContactInformation,
                                                  Address = s.Address

                                              }).FirstOrDefault();
        }

        public IEnumerable<SupplierViewModel> GetAll()
        {
            return _unitOfWork.Suppliers.GetAll().Select(s => new SupplierViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                Address = s.Address,
                ContactInformation = s.ContactInformation
            }).AsEnumerable();
        }

        public void Update(SupplierViewModel supplierViewModel)
        {
            SupplierEntity supplier = new SupplierEntity()
            {
                Id = supplierViewModel.Id,
                Code = supplierViewModel.Code,
                Address = supplierViewModel.Address,
                Name = supplierViewModel.Name,
                ContactInformation = supplierViewModel.ContactInformation
            };
            _unitOfWork.Suppliers.Update(supplier);
            _unitOfWork.Commit();
        }

        public IEnumerable<SupplierViewModel> GetSuppliers()
        {
            return _unitOfWork.Suppliers.GetSupplier();
        }

        public bool IsAlreadyExist(SupplierViewModel SupplierViewModel)
        {
            return _unitOfWork.Suppliers.IsAlreadyExist(SupplierViewModel.Code, SupplierViewModel.Name);
        }

        public string GetNextSupplierCode()
        {
            var lastCode = _unitOfWork.Suppliers.GetNextSupplierCode();
            if(lastCode != null)
            {
                int newCode = int.Parse(lastCode) + 1;
                return newCode.ToString("D3"); ;
            }
            else
            {
                return "001";
            }
        }
    }
}
