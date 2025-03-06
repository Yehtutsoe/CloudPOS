using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using CloudPOS.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleOrderService _saleService;
        private readonly IProductService _productService;
        private readonly ISaleItemService _saleItemService;

        public SaleController(ISaleOrderService saleService, IProductService productService,ISaleItemService saleItemService)
        {
            _saleService = saleService;
            _productService = productService;
            _saleItemService = saleItemService;
        }
        public IActionResult Entry()
        {
      //      var prodcuts = _saleItemService.GetActiveProduct();
            _productService.GetAll();
            return View();
        }
        
    }
}
