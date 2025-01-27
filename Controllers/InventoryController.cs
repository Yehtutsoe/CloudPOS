using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;
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
            var inventoryBalance = _inventoryRepository.RetrieveAll().Select(s => new InventoryViewModel
            {
                Id = s.Id,
                Quantity = s.Quantity,
                CreateAt = s.CreatedAt,
                ProductInfo = _productRepository.GetById(s.ProductId).Name
            }).ToList();
            return View(inventoryBalance);
        }
    }
}
