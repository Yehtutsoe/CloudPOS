using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void CreateOrUpdate(InventoryViewModel inventoryViewModel)
        {
            try
            {
                string productId = inventoryViewModel.ProductId;
                string categoryId = inventoryViewModel.CategoryId;
                DateTime earliestDate = inventoryViewModel.EarliestDate;
                var existingInventoryBalance = _unitOfWork.Inventories.GetInventoryByProductAndEarliest(productId,earliestDate);

                if (existingInventoryBalance != null)
                {
                    switch (inventoryViewModel.TransactionType)
                    {
                        case "Income":
                            existingInventoryBalance.Quantity += inventoryViewModel.Quantity;
                            break;
                        case "Damage":
                        case "Lost":
                            if (inventoryViewModel.Quantity > existingInventoryBalance.Quantity)
                            {
                                throw new Exception("New quantity cannot be greater than the existing quantity.");
                            }
                            existingInventoryBalance.Quantity -= inventoryViewModel.Quantity;
                            if (existingInventoryBalance.Quantity < 0) existingInventoryBalance.Quantity = 0;
                            break;
                        case "Adjustment":
                            // Allow adjustment only if new quantity is less than or equal to old quantity
                            if (inventoryViewModel.Quantity > existingInventoryBalance.Quantity)
                            {
                                throw new Exception("New quantity cannot be greater than the existing quantity.");

                            }
                            existingInventoryBalance.Quantity = inventoryViewModel.Quantity; // Direct Adjustment
                            break;
                        default:
                            throw new Exception("Invalid transaction type");
                    }
                    _unitOfWork.Inventories.Update(existingInventoryBalance);
                    StockLedgerEntity stockLedgerEntity = new StockLedgerEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedAt = DateTime.Now,
                        IsActive = true,
                        LedgerDate = DateTime.Now,
                        Quantity = inventoryViewModel.Quantity,
                        ProductId = productId,
                        TransactionType = inventoryViewModel.TransactionType,
                        EarliestDate = earliestDate

                    };
                    _unitOfWork.StockLedgers.Create(stockLedgerEntity);
                }
                else
                {
                    if (inventoryViewModel.TransactionType == "Income")
                    {
                        InventoryEntity inventoryEntity = new InventoryEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            CreatedAt = DateTime.Now,
                            IsActive = true,
                            ProductId = productId,
                            Quantity = inventoryViewModel.Quantity,
                            EarliestDate = earliestDate,
                            CategoryId = categoryId
                        };
                        _unitOfWork.Inventories.Create(inventoryEntity);
                        StockLedgerEntity stockLedgerEntity = new StockLedgerEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Quantity = inventoryViewModel.Quantity,
                            CreatedAt = DateTime.Now,
                            IsActive = true,
                            LedgerDate = DateTime.Now,
                            ProductId = productId,
                            SourceId = inventoryEntity.Id,
                            TransactionType = inventoryViewModel.TransactionType,
                            EarliestDate = earliestDate
                        };
                        _unitOfWork.StockLedgers.Create(stockLedgerEntity);
                    }
                    else
                    {
                        throw new Exception("Cannot add non-Income transaction on a non-existing stock balance record");
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error in CreateOrUpdate method: {ex.Message}", ex);
            }
            _unitOfWork.Commit();
        }

        public IEnumerable<InventoryViewModel> GetAll(string productId, string categoryId)
        {
                                        var inventory = (from ivn in _unitOfWork.Inventories.GetAll()
                                                         where ivn.IsActive
                                                         join product in _unitOfWork.Products.GetAll()
                                                         on ivn.ProductId equals product.Id
                                                         join category in _unitOfWork.Categories.GetAll()
                                                         on ivn.CategoryId equals category.Id
                                                         select new InventoryViewModel
                                                         {
                                                             Id = ivn.Id,
                                                             CategoryId = category.Id,
                                                             ProductId = product.Id,
                                                             ProductName = product.Name,
                                                             ProductCode = product.Code,
                                                             CategoryName = category.Name,
                                                             CreateAt = ivn.CreatedAt,
                                                             ModifiedAt = ivn.CreatedAt,
                                                             Quantity = ivn.Quantity,
                                                             EarliestDate = ivn.EarliestDate,
                                                             
                                                         }).AsEnumerable();
            //Console.WriteLine($"[DEBUG] Total Inventory Records Retrieved: {inventory.Count()}");

            if (!string.IsNullOrEmpty(productId))
            {
                inventory = inventory.Where(inv => inv.ProductId == productId);
              //  Console.WriteLine($"[DEBUG] Filtered by ProductId: {productId}, Count: {inventory.Count()}");

            }
            if (!string.IsNullOrEmpty(categoryId))
            {
                inventory = inventory.Where(inv => inv.CategoryId == categoryId);
              //  Console.WriteLine($"[DEBUG] Filtered by CategoryId: {categoryId}, Count: {inventory.Count()}");

            }
            return inventory;
        }
    }
}