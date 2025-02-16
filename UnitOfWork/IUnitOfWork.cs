using CloudPOS.Repositories;

namespace CloudPOS.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Categorys { get; }
        IBrandRepository Brands { get; }    
        IProductRepository Products { get; }
        ISaleRepository Sales { get; }
        ISaleItemRepository SaleItems { get; }
        IStockIncomeRepository StockIncomes { get; }
        int Commit(); // Save changes to the databases
    }
}
