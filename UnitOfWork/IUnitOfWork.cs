using CloudPOS.Repositories.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace CloudPOS.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Categories { get; }
        IBrandRepository Brands { get; }    
        IProductRepository Products { get; }
        ISaleOrderRepository Sales { get; }
        ISaleItemRepository SaleItems { get; }
        IPurchaseRepository Purchases { get; }
        IPurchaseDetailRepository PurchaseDetails { get; }
        ISupplierRepository Suppliers { get; }
        IInventoryRepository Inventories { get; }
        IStockledgerRepository StockLedgers { get; }
        IPriceRepository Prices { get; }

        void Commit(); // Save changes to the databases
        void Rollback();
        IDbContextTransaction BeginTransaction();
    }
}
