using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;

        public SaleController(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }
        public async Task<IActionResult> Entry()
        {
            var saleViewModel = new SaleProcessViewModel()
            {
                ProductViewModels = GetProductList()
            };
            return View(saleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Entry(SaleProcessViewModel saleViewModel)
        {
            await _saleService.Create(saleViewModel);
            return RedirectToAction("List");
        }
    }
}
