using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InventoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(InventoryEntity inventoryEntity)
        {
           _applicationDbContext.Inventories.Add(inventoryEntity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var existingEntity = _applicationDbContext.Inventories.Find(Id);
            if (existingEntity != null) { 
                _applicationDbContext.Inventories.Remove(existingEntity);
                _applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<InventoryEntity> GetById(string Id)
        {
            return _applicationDbContext.Inventories.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<InventoryEntity> RetrieveAll()
        {
            return _applicationDbContext.Inventories.ToList();
        }

        public void Update(InventoryEntity inventoryEntity)
        {
            var existingInventory = _applicationDbContext.Inventories.Find(inventoryEntity.Id);
            if (existingInventory != null)
            {
                _applicationDbContext.Entry(existingInventory).CurrentValues.SetValues(inventoryEntity);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
