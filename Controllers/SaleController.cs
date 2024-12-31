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
        public async Task<IActionResult> List()
        {
            var sales = await _saleService.GetAllSale();

            return View(sales);
        }
        public IActionResult Edit(string Id)
        {
            return View(_saleService.GetById(Id));
        }
        public IActionResult Delete(string Id)
        {
            _saleService.Delete(Id);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Update(SaleProcessViewModel saleViewModel)
        {
            _saleService.Update(saleViewModel);
            return RedirectToAction("List");
        }

        private IList<ProductViewModel> GetProductList()
        {
            // Example: Fetch products from the database or repository
            return _productService.RetrieveAll().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
    }
}
