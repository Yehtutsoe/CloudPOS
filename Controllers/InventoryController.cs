using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;

        public InventoryController(IInventoryRepository inventoryRepository,IProductRepository productRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
        }
        public IActionResult List()
        {
            var inventoryBalance = _inventoryRepository.GetAll().Select(s => new InventoryViewModel
            {
                Id = s.Id,
                Quantity = s.Quantity,
                CreateAt = s.CreatedAt,
                
            }).ToList();
            return View(inventoryBalance);
        }
    }
}
