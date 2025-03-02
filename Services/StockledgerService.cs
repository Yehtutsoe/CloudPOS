
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace CloudPOS.Services
{
    public class StockledgerService : IStockledgerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockledgerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<StockLedgerViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? productId = null)
        {
            var stocks = (from sl in _unitOfWork.StockLedgers.GetAll().ToList()
                          join p in _unitOfWork.Products.GetAll().ToList()
                          on sl.ProductId equals p.Id into products
                          from p in products.DefaultIfEmpty()
                          select new StockLedgerViewModel
                          {
                              id = sl.Id,
                              ProductId = sl.ProductId,
                              ProductCode = p?.Code ?? "N/A",
                              ProductName = p?.Name ?? "N/A",
                              Quantity = sl.Quantity,
                              LedgerDate = sl.LedgerDate,
                              TransactionType = sl.TransactionType ?? "Unknow",
                              InQty = (sl.TransactionType == "Income" || sl.TransactionType == "Purchase") ? (sl.Quantity) : 0,
                              OutQty = (sl.TransactionType == "Damage" || sl.TransactionType == "Lost" || sl.TransactionType == "Adjustment" || sl.TransactionType == "Sale") || sl.TransactionType == "Purchase Delete" ? (sl.Quantity) : 0 * -1

                          });
            if(fromDate.HasValue && toDate.HasValue)
            {
                stocks = stocks.Where(sl => sl.LedgerDate >= fromDate.Value && sl.LedgerDate <= toDate.Value); 

            }        
            if(!string.IsNullOrEmpty(productId) && productId != "Select an Product")
            {
                stocks = stocks.Where(sl => sl.ProductId == productId);
            }
            return stocks.ToList();
        }
    }
}
