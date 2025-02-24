using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;
using Microsoft.AspNetCore.Mvc;

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

        public bool ReduceForSale(string productId, int quantity)
        {
            var inventory = _dbContext.Inventories.FirstOrDefault(v => v.ProductId == productId);
            if (inventory == null)
            {
                throw new Exception("Not found the product in Inventory");
            }
            if (inventory.Quantity < quantity)
            {
                throw new Exception("Not enought quantity of product");
            }
            inventory.Quantity -= quantity;
            inventory.AdjustmentDate = DateTime.Now;
            _dbContext.SaveChanges();
            return true;
        }

        public void UpdateInventoryBalanceByProduct(string productId, string categoryId, int quantity)
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
