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
            _unitOfWork = unitOfWork;
        }

        public void CreateOrUpdate(InventoryViewModel inventoryViewModel)
        {
            try
            {
                var existingInventoryBalance =_unitOfWork.Inventories.GetInventoryByProduct(inventoryViewModel.ProductId);

                if (existingInventoryBalance != null)
                {
                     UpdateExistingInventory(existingInventoryBalance, inventoryViewModel);
                }
                else
                {
                    if (inventoryViewModel.TransactionType == "Income")
                    {
                        CreateNewInventory(inventoryViewModel);
                    }
                    else
                    {
                        throw new InvalidOperationException("Cannot add non-Income transaction on a non-existing stock balance record.");
                    }
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CreateOrUpdateAsync: {ex.Message}", ex);
            }
        }

        private void UpdateExistingInventory(InventoryEntity inventory, InventoryViewModel model)
        {
            switch (model.TransactionType)
            {
                case "Income":
                    inventory.Quantity += model.Quantity;
                    break;
                case "Damage":
                case "Lost":
                case "Adjustment":
                    if (model.Quantity > inventory.Quantity)
                    {
                        throw new InvalidOperationException("New quantity cannot be greater than the existing quantity.");
                    }
                    inventory.Quantity = model.TransactionType == "Adjustment" ? model.Quantity : inventory.Quantity - model.Quantity;
                    break;
                default:
                    throw new ArgumentException("Invalid transaction type.");
            }

            _unitOfWork.Inventories.Update(inventory);
            CreateStockLedger(model);
        }

        private void CreateNewInventory(InventoryViewModel model)
        {
            var inventoryEntity = new InventoryEntity
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                ProductId = model.ProductId,
                CategoryId = model.CategoryId,
                Quantity = model.Quantity
            };

            _unitOfWork.Inventories.Create(inventoryEntity);
            CreateStockLedger(model);
        }

        private void CreateStockLedger(InventoryViewModel model)
        {
            var stockLedgerEntity = new StockLedgerEntity
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                LedgerDate = DateTime.UtcNow,
                Quantity = model.Quantity,
                ProductId = model.ProductId,
                TransactionType = model.TransactionType
            };

            _unitOfWork.StockLedgers.Create(stockLedgerEntity);
        }

        public IEnumerable<InventoryViewModel> GetAll(string productId, string categoryId)
        {
            var inventories = _unitOfWork.Inventories.GetAll();
            var products =_unitOfWork.Products.GetAll();
            var categories = _unitOfWork.Categories.GetAll();

            var inventoryList = (from ivn in inventories
                                 join product in products on ivn.ProductId equals product.Id
                                 join category in categories on ivn.CategoryId equals category.Id
                                 select new InventoryViewModel
                                 {
                                     Id = ivn.Id,
                                     CategoryId = category.Id,
                                     ProductId = product.Id,
                                     ProductCode =product.Code,
                                     ProductName = product.Name,
                                     CategoryName = category.Name,
                                     CreateAt = ivn.CreatedAt,
                                     Quantity = ivn.Quantity
                                 }).AsQueryable();

            if (!string.IsNullOrEmpty(productId))
            {
                inventoryList = inventoryList.Where(inv => inv.ProductId == productId);
            }

            if (!string.IsNullOrEmpty(categoryId)) // Previously, you were checking for an empty string incorrectly
            {
                inventoryList = inventoryList.Where(inv => inv.CategoryId == categoryId);
            }

            return inventoryList.ToList();
        }
    }
}
