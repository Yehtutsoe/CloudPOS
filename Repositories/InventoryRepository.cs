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
           _applicationDbContext.Inventorys.Add(inventoryEntity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var existingEntity = _applicationDbContext.Inventorys.Find(Id);
            if (existingEntity != null) { 
                _applicationDbContext.Inventorys.Remove(existingEntity);
                _applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<InventoryEntity> GetById(string Id)
        {
            return _applicationDbContext.Inventorys.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<InventoryEntity> RetrieveAll()
        {
            return _applicationDbContext.Inventorys.ToList();
        }

        public void Update(InventoryEntity inventoryEntity)
        {
            var existingInventory = _applicationDbContext.Inventorys.Find(inventoryEntity.Id);
            if (existingInventory != null)
            {
                _applicationDbContext.Entry(existingInventory).CurrentValues.SetValues(inventoryEntity);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
