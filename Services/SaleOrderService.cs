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
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    // Create Sale Order
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
                        TotalReturnAmount = model.SaleOrder.TotalReturnAmount,
                        SaleType = model.SaleType,
                        UserId = model.UserId
                    };
                    _unitOfWork.Sales.Create(saleEntity);

                    foreach (var details in model.SaleDetails)
                    {
                        if (string.IsNullOrEmpty(details.ProductId))
                        {
                            //Console.WriteLine("ProductId is null or empty for one of the sale items!");
                            throw new InvalidOperationException("ProductId is missing for one of the sale items.");
                        }

                        if (model.StockSwitch)
                        {
                            var stockAvailable = _unitOfWork.Inventories.GetAvaliableStock(details.ProductId);
                            if (stockAvailable < details.Quantity)
                            {
                                throw new InvalidOperationException($"Stock is not enough for item: {details.ProductId}. Required: {details.Quantity}, Available: {stockAvailable}");
                            }

                            // Reduce Stock & Add StockLedger at the same time
                            var stockUsages = _unitOfWork.Inventories.ReduceForSale(details.ProductId, details.Quantity);
                            foreach (var usage in stockUsages)
                            {
                                StockLedgerEntity stockLedgerEntity = new StockLedgerEntity()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    IsActive = true,
                                    CreatedAt = DateTime.Now,
                                    ProductId = details.ProductId,
                                    LedgerDate = DateTime.Now,
                                    Quantity = usage.QuantityUsed,
                                    EarliestDate = DateTime.Parse(usage.EarliestDate),
                                    TransactionType = "Sale"
                                };
                                _unitOfWork.StockLedgers.Create(stockLedgerEntity);
                            }
                        }

                        // Create Sale Item
                        SaleItemEntity saleItemEntity = new SaleItemEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            CreatedAt = DateTime.Now,
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
                    }
                    _unitOfWork.Commit();
                    transaction.Commit(); //Commit after all operations
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Rollback on error
                    throw;
                }
            }
        }

        public IEnumerable<SaleOrderViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? voucherNo = null)
        {
            var query = _unitOfWork.Sales.GetAll().AsQueryable();

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
    }
}
