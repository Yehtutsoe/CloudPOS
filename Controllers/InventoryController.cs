using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public IActionResult List()
        {
            var inventoryBalance = _inventoryRepository.GetAll().Select(s => new InventoryViewModel
            {
                Id = s.Id,
                ProductName = s.Products.Name,
                Quantity = s.Quantity,
                CreateAt = s.CreatedAt,
                
            }).ToList();
            return View(inventoryBalance);
        }
    }
}
