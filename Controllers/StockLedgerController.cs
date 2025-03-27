using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class StockLedgerController : Controller
    {
        private readonly IStockledgerService _stockledgerService;
        private readonly IProductService _productService;

        public StockLedgerController(IStockledgerService stockledgerService,IProductService productService)
        {
            _stockledgerService = stockledgerService;
            _productService = productService;
        }
        [HttpGet]
        public IActionResult Entry()
        {
            ProductBinding();
            return View();
        }
        public IActionResult List(DateTime? fromDate,DateTime? toDate,string? productId)
        {
            var stock=_stockledgerService.GetAll(fromDate,toDate,productId);
            ProductBinding();
            return View(stock);
        }

        private void ProductBinding()
        {
            var product = _productService.GetProducts();
            ViewBag.Products = product;
        }
    }
}
