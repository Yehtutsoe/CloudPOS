using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IStockledgerService
    {
        IEnumerable<StockLedgerViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? productId = null);
    }
}
