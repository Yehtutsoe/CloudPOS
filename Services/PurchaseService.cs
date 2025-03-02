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
            this._unitOfWork = unitOfWork;
        }
        public void Create(PurchaseWithDetailViewModel models)
        {
            var purchaseViewModels = models.Purchase;
            var purchaseDetailViewModels = models.ProductDetails;
            decimal purchaseTotal = 0;
            foreach (var i in purchaseDetailViewModels) 
            {
                purchaseTotal += i.TotalPrice;
            }
            PurchaseEntity purchaseEntity = new PurchaseEntity()
            {
                Id = Guid.NewGuid().ToString(),
                PurchaseDate = purchaseViewModels.PurchaseDate,
                SupplierId = purchaseViewModels.SupplierId,
                VoucherNo = purchaseViewModels.VoucherNo,
                CreatedAt = DateTime.Now,
                TotalCost = purchaseViewModels.TotalAmount,
                Deliverystatus = purchaseViewModels.Deliverystatus,
                IsActive = true,
            };
            _unitOfWork.Purchases.Create(purchaseEntity);
            foreach(var detail in purchaseDetailViewModels)
            {
                PurchaseDetailEntity purchaseDetailEntity = new PurchaseDetailEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                    ProductId = detail.ProductId,
                    PurchaseId = purchaseEntity.Id,
                    TotalPrice = detail.TotalPrice,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                };
                var categoryId = _unitOfWork.Products.FindById(detail.ProductId)?.CategoryId;
                _unitOfWork.Inventories.UpdateInventoryBalanceByProduct(detail.ProductId, detail.Quantity,categoryId);
                StockLedgerEntity stockLedgerEntity = new StockLedgerEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    LedgerDate = DateTime.Now,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    TransactionType = "Purchase",
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                _unitOfWork.StockLedgers.Create(stockLedgerEntity);
                _unitOfWork.PurchaseDetails.Create(purchaseDetailEntity);
            }
            _unitOfWork.Commit();
        }

        public bool Delete(string Id)
        {
            var trasaction = _unitOfWork.BeginTransaction();
            try
            {
                var purchase = _unitOfWork.Purchases.FindById(Id);
                if (purchase == null)
                {
                    return false;
                }
                _unitOfWork.Purchases.Update(purchase);
                var purchaseDetails = _unitOfWork.PurchaseDetails.FindByPurchaseId(purchase.Id).ToList();
                foreach(var detail in purchaseDetails)
                {                                                                       // Decrease quantity since purchase is deleted
                    _unitOfWork.Inventories.UpdateInventoryBalanceByProduct(detail.ProductId, detail.Quantity,null); // You can pass category ID if required

                    // Remove associated stock ledger entries
                    StockLedgerEntity stockLedger = new StockLedgerEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        LedgerDate = DateTime.Now,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        CreatedAt = DateTime.Now,
                        IsActive = true,
                        TransactionType = "Purchase Delete"
                    };
                    _unitOfWork.StockLedgers.Create(stockLedger);
                }
                _unitOfWork.PurchaseDetails.DeleteRange(purchaseDetails);
                _unitOfWork.Commit();
                trasaction.Commit();
                return true;
            }
            catch (Exception)
            {

                trasaction.Rollback();
                throw;
            }
   
        }

        public IEnumerable<PurchaseViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? supplierId = null, string? purchaseId = null)
        {
            var purchase = (from p in _unitOfWork.Purchases.GetAll()
                            join s in _unitOfWork.Suppliers.GetAll()
                            on p.SupplierId equals s.Id
                            select new PurchaseViewModel()
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
                purchase = purchase.Where(s =>s.PurchaseDate != null && s.PurchaseDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                purchase = purchase.Where(s => s.PurchaseDate != null && s.PurchaseDate <= toDate.Value);
            }
            if(!string.IsNullOrEmpty(supplierId) && supplierId != "Select an Supplier")
            {
                purchase = purchase.Where(s => s.SupplierId == supplierId);
            }
            if(!string.IsNullOrEmpty(purchaseId) && purchaseId !="Select an VoucherNo")
            {
                purchase = purchase.Where(s =>s.Id == purchaseId);
            }
            return purchase.ToList();
        }

        public IEnumerable<PurchaseViewModel> GetPurchase()
        {
            return _unitOfWork.Purchases.GetPurchase().ToList();
        }
    }
}
