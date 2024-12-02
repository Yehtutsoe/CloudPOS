using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface ISupplierRepository
    {
        void Create(SupplierEntity supplier);
        IEnumerable<SupplierEntity> RetrieveAll();
        IEnumerable<SupplierEntity> GetById(string Id);
        void Delete(string Id);
        void Update(SupplierEntity supplier);
    }
}
