using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface ISupplierService
    {
        void Create(SupplierViewModel supplierViewModel);
        IEnumerable<SupplierViewModel> RetrieveAll();
        SupplierViewModel GetById(string Id);
        void Delete(string Id);
        void Update(SupplierViewModel supplierViewModel);

    }
}
