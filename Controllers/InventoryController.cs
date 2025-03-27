using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public InventoryController(IInventoryService inventoryService,IProductService productService,ICategoryService categoryService)
        {
            _inventoryService = inventoryService;
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetProduct(string categoryId)
        {
            var products = _productService.GetProductByCategory(categoryId);
            return Json(products);
        }
        public IActionResult Entry()
        {
            LoadDropdownData();            
            return View();
        }
        [HttpPost]
        public IActionResult Entry(InventoryViewModel ui)
        {
            if (!ModelState.IsValid)
            {
                // Debugging: Show model state errors in console
                foreach (var modelError in ModelState)
                {
                    foreach (var error in modelError.Value.Errors)
                    {
                        Console.WriteLine($"Field: {modelError.Key}, Error: {error.ErrorMessage}");
                    }
                }

                LoadDropdownData();
                return View(ui);
            }
            try
            {
                _inventoryService.CreateOrUpdate(ui);
                ViewData["Info"] = "Successfully Save the record to database";
                ViewData["Status"] = true;
            }
            catch (Exception ex)
            {
                ViewData["Info"] = "Can not Save the record to database" + ex.Message;
                ViewData["Status"] = false;
            }
            LoadDropdownData();
            return View(ui);
        }
        public IActionResult List(string? productId,string? categoryId)
        {
            var inventory =   _inventoryService.GetAll(productId, categoryId);
            LoadDropdownData();
            return View(inventory);
        }

        private void LoadDropdownData()
        {
            ViewBag.Products = _productService.GetProducts();
            ViewBag.Categories = _categoryService.GetCategories();
        }
    }
}
