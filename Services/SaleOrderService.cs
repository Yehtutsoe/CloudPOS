using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(SaleWithSaleItemViewModel model)
        {
            SaleEntity saleEntity = new SaleEntity()
            {
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                SaleDate = model.SaleOrder.SaleDate,
                VoucherNo = model.SaleOrder.VoucherNo,
                Balance = model.SaleOrder.Balance,
                CashAmount = model.SaleOrder.CashAmount,
                CreatedAt = DateTime.Now,
                DiscountAmount = model.SaleOrder.DiscountAmount,
                NetTotal = model.SaleOrder.NetTotal,
                SubTotal = model.SaleOrder.NetTotal,
                TotalAmount = model.SaleOrder.TotalAmount,
                SaleType = model.SaleType
            };
            _unitOfWork.Sales.Create(saleEntity);
            foreach(var details in model.SaleDetails)
            {
                if (model.StockSwitch)
                {
                    var stockAvailable = _unitOfWork.Inventories.GetAvaliableStock(details.ProductId);
                    if(stockAvailable < details.Quantity)
                    {
                        throw new InvalidOperationException($"Stock is not enough for item: {details.ProductId}. Required: {details.Quantity}, Available: {stockAvailable}");

                    }
                    var useBatches = _unitOfWork.Inventories.ReduceForSale(details.ProductId,details.Quantity);
                    foreach (var batch in useBatches) 
                    {
                        StockLedgerEntity stockLedgerEntity = new StockLedgerEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            IsActive = true,
                            CreatedAt = DateTime.Now,
                            ProductId = details.ProductId,
                            LedgerDate = DateTime.Now,
                            Quantity = batch.QuantityUsed,
                            TransactionType = "Sale"                            
                        };
                        _unitOfWork.StockLedgers.Create(stockLedgerEntity);

                    }
                }
                SaleItemEntity saleItemEntity = new SaleItemEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt= DateTime.Now,
                    DiscountAmount = details.DiscountAmount,
                    IsActive = true,
                    ProductId = details.ProductId,
                    Price = details.Price,
                    Quantity = details.Quantity,
                    TotalPrice = details.Total,
                    SaleAmount = details.Amount,
                    SaleId = saleEntity.Id
                    
                };
                _unitOfWork.SaleItems.Create(saleItemEntity);
                _unitOfWork.Commit();
            }
        }

        public IEnumerable<SaleOrderViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? voucherNo = null)
        {
            var sales = _unitOfWork.Sales.GetAll(); // Ensure this returns IQueryable<Sale>

            if (sales is IQueryable<SaleEntity>) // Ensure filtering is done at the database level
            {
                var query = (IQueryable<SaleEntity>)sales;

                if (fromDate.HasValue)
                {
                    query = query.Where(s => s.SaleDate >= fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    query = query.Where(s => s.SaleDate <= toDate.Value);
                }

                if (!string.IsNullOrEmpty(voucherNo))
                {
                    query = query.Where(s => s.VoucherNo.ToLower() == voucherNo.ToLower());
                }

                return query.Select(s => new SaleOrderViewModel
                {
                    Id = s.Id,
                    SaleDate = s.SaleDate,
                    VoucherNo = s.VoucherNo,
                    CashAmount = s.CashAmount,
                    SaleType = s.SaleType,
                }).ToList(); // Convert to List after filtering
            }
            else
            {
                throw new InvalidOperationException("Sales data source must be queryable.");
            }
        }

    }
}
