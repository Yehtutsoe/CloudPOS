using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public interface ISupplierRepository:IBaseRepository<SupplierEntity>
    {
        SupplierEntity FindById(string Id);
        IEnumerable<SupplierViewModel> GetSupplier();
        bool IsAlreadyExist(string code, string name);
        string GetNextSupplierCode();

    }
}
