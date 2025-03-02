using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ISupplierService
    {
        void Create(SupplierViewModel supplierViewModel);
        IEnumerable<SupplierViewModel> GetAll();
        SupplierViewModel GetById(string Id);
        void Delete(string Id);
        void Update(SupplierViewModel supplierViewModel);
        IEnumerable<SupplierViewModel> GetSuppliers();
        bool IsAlreadyExist(SupplierViewModel SupplierViewModel);
        string GetNextSupplierCode();

    }
}
