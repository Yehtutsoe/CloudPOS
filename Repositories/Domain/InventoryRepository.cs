using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class InventoryRepository : BaseRepository<InventoryEntity>, IInventoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InventoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetAvaliableStock(string productId)
        {
            return _dbContext.Inventories.Where(iv => iv.ProductId == productId).Sum(iv => iv.Quantity);
        }

        public InventoryEntity GetInventoryByProduct(string productId)
        {
            return _dbContext.Inventories.FirstOrDefault(iv => iv.ProductId == productId);
        }

        public List<(string? ProductId, int QuantityUsed)> ReduceForSale(string productId, int quantity)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }

            // Fetch inventory record
            var inventory = _dbContext.Inventories.FirstOrDefault(v => v.ProductId == productId);
            if (inventory == null)
            {
                throw new KeyNotFoundException("Product not found in inventory.");
            }

            if (inventory.Quantity < quantity)
            {
                throw new InvalidOperationException("Not enough stock available.");
            }

            // Track used quantities
            var usedQuantities = new List<(string? ProductId, int QuantityUsed)>();

            // Deduct stock and record usage
            int usedQuantity = Math.Min(inventory.Quantity, quantity);
            inventory.Quantity -= usedQuantity;
            inventory.AdjustmentDate = DateTime.UtcNow;

            usedQuantities.Add((productId, usedQuantity));

            _dbContext.SaveChanges();

            return usedQuantities;
        }




        public void UpdateInventoryBalanceByProduct(string productId, int quantity, string categoryId)
        {
            var inventoryEntity = _dbContext.Inventories.FirstOrDefault(v => v.ProductId == productId && v.CategoryId == categoryId);
            if (inventoryEntity != null)
            {
                inventoryEntity.Quantity += quantity;
                inventoryEntity.AdjustmentDate = DateTime.Now;
                if (inventoryEntity.Quantity < 0)
                {
                    throw new InvalidOperationException($"Insufficient stock for Product: {productId} and Cannot reduce stock below zero.");
                }
                _dbContext.Inventories.Update(inventoryEntity);
            }
            else
            {
                if (quantity < 0)
                {
                    // Trying to decrease stock for a non-existing entry
                    throw new InvalidOperationException($"No stock balance found for Product: {productId} by {Math.Abs(quantity)}.");
                }
                inventoryEntity = new InventoryEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Quantity = quantity,
                    CategoryId = categoryId,
                    ProductId = productId,
                    AdjustmentDate = DateTime.Now,
                    CreatedAt = DateTime.Now
                };
                _dbContext.Inventories.Add(inventoryEntity);
            }
            _dbContext.SaveChanges();
        }
    }
}
