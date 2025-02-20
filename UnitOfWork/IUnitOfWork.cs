using CloudPOS.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloudPOS.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Categories { get; }
        IBrandRepository Brands { get; }    
        IProductRepository Products { get; }
        ISaleRepository Sales { get; }
        ISaleItemRepository SaleItems { get; }
        IStockIncomeRepository StockIncomes { get; }
        ISupplierRepository Suppliers { get; }
        void Commit(); // Save changes to the databases
        void Rollback();
        IDbContextTransaction BeginTransaction();
    }
}
