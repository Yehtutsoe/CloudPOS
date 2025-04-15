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

        public InventoryEntity GetInventoryByProductAndEarliest(string productId,DateTime earliestDate)
        {
            return _dbContext.Inventories.FirstOrDefault(iv => iv.ProductId == productId && iv.EarliestDate.Date == earliestDate.Date);
        }

        public List<(string EarliestDate, int QuantityUsed)> ReduceForSale(string productId, int quantity)
        {

            var stockBalances = _dbContext.Inventories
                                          .Where(sb => sb.ProductId == productId && sb.Quantity == quantity)
                                          .OrderBy(sb=> sb.EarliestDate)
                                          .ToList();
            var usedBatch = new List<(string EarliestDate, int QuantityUsed)>();
            foreach(var stockBalance in stockBalances)
            {
                if (quantity <= 0)
                    break;
                int usedQuantity = 0;
            if(stockBalance.Quantity >= quantity)
                {
                    usedQuantity = quantity;
                    stockBalance.Quantity -= quantity;
                    quantity = 0;
                }
                else
                {
                    usedQuantity = quantity;
                    quantity -= stockBalance.Quantity;
                    stockBalance.Quantity = 0;
                }
                _dbContext.Inventories.Update(stockBalance);
                usedBatch.Add((EarliestDate:stockBalance.EarliestDate.ToString(), QuantityUsed:usedQuantity));
            }
            if (quantity > 0)
            {
                throw new InvalidOperationException("Not enough stock available.");
            }

            _dbContext.SaveChanges();
            return usedBatch;
        }

        public void UpdateInventoryBalanceByProductAndEarliest(string productId, int quantity, string categoryId,DateTime earliestDate)
        {
            var inventoryEntity = _dbContext.Inventories.FirstOrDefault(v => v.ProductId == productId && 
                                                                             v.CategoryId == categoryId &&
                                                                             v.EarliestDate.Date == earliestDate.Date);
            if (inventoryEntity != null)
            {
                inventoryEntity.Quantity += quantity;
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
                    EarliestDate = earliestDate,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    
                };
                _dbContext.Inventories.Add(inventoryEntity);
            }
            _dbContext.SaveChanges();
        }

        public override IEnumerable<InventoryEntity> GetAll() => _dbContext.Inventories.Where(i => i.IsActive).ToList();

    }
}
