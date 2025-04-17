using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PurchaseWithDetailViewModel models)
        {
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                var purchaseViewModels = models.Purchase;
                var purchaseDetailViewModels = models.ProductDetails;
                var currentTime = DateTime.UtcNow;

                decimal purchaseTotal = purchaseDetailViewModels.Sum(i => i.TotalPrice);

                var purchaseEntity = new PurchaseEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    PurchaseDate = purchaseViewModels.PurchaseDate,
                    SupplierId = purchaseViewModels.SupplierId,
                    VoucherNo = purchaseViewModels.VoucherNo,
                    CreatedAt = currentTime,
                    TotalCost = purchaseTotal,
                    Deliverystatus = purchaseViewModels.Deliverystatus,
                    IsActive = true,
                };

                _unitOfWork.Purchases.Create(purchaseEntity);

                foreach (var detail in purchaseDetailViewModels)
                {
                    var purchaseDetailEntity = new PurchaseDetailEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Quantity = detail.Quantity,
                        Price = detail.Price,
                        ProductId = detail.ProductId,
                        PurchaseId = purchaseEntity.Id,
                        TotalPrice = detail.TotalPrice,
                        IsActive = true,
                        CreatedAt = currentTime,
                        EarliestDate = detail.EarliestDate
                    };

                    var product = _unitOfWork.Products.FindById(detail.ProductId);
                    var categoryId = product?.CategoryId;

                    _unitOfWork.Inventories.UpdateInventoryBalanceByProductAndEarliest(
                        detail.ProductId,
                        detail.Quantity,
                        categoryId,
                        detail.EarliestDate // EarliestDate passed as DateTime
                    );

                    var stockLedgerEntity = new StockLedgerEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        LedgerDate = currentTime,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        TransactionType = "Purchase",
                        CreatedAt = currentTime,
                        IsActive = true,
                        SourceId = purchaseEntity.Id,
                        EarliestDate = detail.EarliestDate //Stored with correct date
                    };

                    _unitOfWork.StockLedgers.Create(stockLedgerEntity);
                    _unitOfWork.PurchaseDetails.Create(purchaseDetailEntity);
                }

                _unitOfWork.Commit();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Failed to create purchase transaction", ex);
            }
        }

        public bool Delete(string Id)
        {
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                var purchase = _unitOfWork.Purchases.FindById(Id);
                if (purchase == null)
                    return false;

                // Optionally mark inactive
                //_unitOfWork.Purchases.Update(purchase);

                var purchaseDetails = _unitOfWork.PurchaseDetails.FindByPurchaseId(Id).ToList();

                foreach (var detail in purchaseDetails)
                {
                    _unitOfWork.Inventories.UpdateInventoryBalanceByProductAndEarliest(
                        detail.ProductId,
                        -detail.Quantity,
                        null,
                        detail.EarliestDate 
                    );

                    var stockLedger = new StockLedgerEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        LedgerDate = DateTime.UtcNow,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        CreatedAt = DateTime.UtcNow,
                        EarliestDate = detail.EarliestDate,
                        IsActive = true,
                        TransactionType = "Purchase Delete"
                    };

                    _unitOfWork.StockLedgers.Create(stockLedger);
                }

                _unitOfWork.PurchaseDetails.DeleteRange(purchaseDetails);
                _unitOfWork.Commit();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Failed to delete purchase transaction", ex);
            }
        }

        public IEnumerable<PurchaseViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? supplierId = null, string? purchaseId = null)
        {
            var query = _unitOfWork.Purchases.GetAll().Where(p => p.IsActive)
                .Join(_unitOfWork.Suppliers.GetAll().Where(s => s.IsActive),
                    p => p.SupplierId,
                    s => s.Id,
                    (p, s) => new PurchaseViewModel
                    {
                        Id = p.Id,
                        PurchaseDate = p.PurchaseDate,
                        Deliverystatus = p.Deliverystatus,
                        SupplierId = p.SupplierId,
                        VoucherNo = p.VoucherNo,
                        SupplierInfo = s.Name,
                        TotalAmount = p.TotalCost
                    });

            if (fromDate.HasValue)
            {
                query = query.Where(s => s.PurchaseDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(s => s.PurchaseDate <= toDate.Value);
            }
            if (!string.IsNullOrEmpty(supplierId) && supplierId != "Select an Supplier")
            {
                query = query.Where(s => s.SupplierId == supplierId);
            }
            if (!string.IsNullOrEmpty(purchaseId) && purchaseId != "Select an VoucherNo")
            {
                query = query.Where(s => s.Id == purchaseId);
            }

            return query.ToList();
        }

        public IEnumerable<PurchaseViewModel> GetPurchase()
        {
            return _unitOfWork.Purchases.GetPurchase().ToList();
        }
    }
}
