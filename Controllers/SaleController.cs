using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudPOS.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleOrderService _saleOrderService;
        private readonly IProductService _productService;
        private readonly ISaleItemService _saleItemService;
        private readonly IPriceService _priceService;

        public SaleController(ISaleOrderService saleService,
                              IProductService productService,
                              ISaleItemService saleItemService,
                              IPriceService priceService)
        {
            _saleOrderService = saleService;
            _productService = productService;
            _saleItemService = saleItemService;
            _priceService = priceService;
        }
        public IActionResult Entry()
        {
            BindingProduct();
            BindingSale();  
            return View();
        }
        private void BindingProduct()
        {
            var products = _productService.GetProducts();
            ViewBag.Products = products;
        }
        private void BindingSale()
        {
            var Sales = _saleOrderService.GetAll();
            ViewBag.Sale = Sales;
        }
        [HttpGet]
        public IActionResult GetProductDetails(string barCode)
        {
            if (string.IsNullOrEmpty(barCode))
            {
                return Json(null);
            }

            var details = _priceService.ProductDetailsByBarCode(barCode);

            if (details != null)
            {
                return Json(new
                {
                    ProductId = details.ProductId,
                    RetailSalePrice = details.RetailSalePrice,
                    WholeSalePrice = details.WholeSalePrice
                });
            }

            return Json(null);
        }
        [HttpPost]
        public IActionResult Entry(SaleWithSaleItemViewModel model)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                model.UserId = userId;
                _saleOrderService.Create(model);
                ViewData["Info"] = "Successfully saved data to the system.";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occour,can not save the data to the system."+ e.Message;
                ViewData["status"] = false;
            }
            BindingProduct();
            return View(model);
        }
        public IActionResult List(DateTime? fromDate = null, DateTime? toDate = null,string? VoucherNo = null)
        {var sale=_saleOrderService.GetAll( fromDate ,  toDate , VoucherNo);
            BindingSale();
            return View(sale);
        }
    }
}
