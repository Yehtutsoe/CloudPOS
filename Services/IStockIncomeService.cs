using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IStockIncomeService
    {
        void Create(StockIncomeViewModel purchaseViewModel);
        IEnumerable<StockIncomeViewModel> RetrieveAll();
        void Delete(string Id);
        StockIncomeViewModel GetById(string Id);
        void Update(StockIncomeViewModel purchaseViewModel);
        StockIncomeViewModel GetActiveSuppliersAndProducts ();
        

    }
}
