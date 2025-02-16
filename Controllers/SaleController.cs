using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using CloudPOS.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleProcessService _saleService;
        private readonly IProductService _productService;
        private readonly ISaleItemService _saleItemService;

        public SaleController(ISaleProcessService saleService, IProductService productService,ISaleItemService saleItemService)
        {
            _saleService = saleService;
            _productService = productService;
            _saleItemService = saleItemService;
        }
        public IActionResult Entry()
        {
            var prodcuts = _saleItemService.GetActiveProduct();
            _productService.RetrieveAll();
            return View(prodcuts);
        }
        [HttpPost]
        public IActionResult AddToCart(SaleItemViewModel saleItem)
        {
            ViewBag.Info = "Adding an sale item to cart";
            var products = _productService.GetById(saleItem.ProductId);
            saleItem.ProductInfo = products.Name;
            saleItem.UnitPrice = products.SalePrice;
            if (SessionHelper.GetDataFromSession<List<SaleItemViewModel>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<SaleItemViewModel>();
                cart.Add(saleItem);
               SessionHelper.SetDataToSession(HttpContext.Session, "cart", cart);
            }
            else
            {
                var cart = SessionHelper.GetDataFromSession<List<SaleItemViewModel>>(HttpContext.Session, "cart");
                //check the item is already exists in cart
                int index = IsAlreadyExistInCart(saleItem.ProductId);
                if(index != -1)
                {
                    //when item already have in cart,increase the quantity
                   cart[index].Quantity = cart[index].Quantity + saleItem.Quantity;
                }
                else
                {
                    //if not in cart,add the new item
                    cart.Add(saleItem);
                }
                //add the cart record to the session
                SessionHelper.SetDataToSession(HttpContext.Session, "cart", cart);
            }
            return Json(null);
        }
        public IActionResult CheckCart(SaleViewModel saleView)
        {
            var cart = SessionHelper.GetDataFromSession<List<SaleItemViewModel>>(HttpContext.Session, "cart");
            if (cart == null || !cart.Any())
            { // Handle the case where the cart is null or empty
                TempData["TotalAmount"] = "0";
                ViewBag.total = 0;
                return View(new List<SaleItemViewModel>());
            }
                TempData["SaleDate"] = saleView.SaleDate;
            TempData["VoucherNo"] = saleView.VoucherNo;
            decimal total = cart.Sum(item => item.UnitPrice * item.Quantity);
            TempData["TotalAmount"] = total.ToString("F2");
            ViewBag.total = total;
            return View(cart);
        }
        [HttpPost]
        public IActionResult Paid(SaleViewModel sale)
        {
            try
            {
                var cart = SessionHelper.GetDataFromSession<List<SaleItemViewModel>>(HttpContext.Session, "cart");
                foreach (var saleItems in cart)
                {
                    _saleService.Create(sale, saleItems);
                }
                SessionHelper.ClearSession(HttpContext.Session);
            }
            catch (Exception e)
            {

                TempData["Inof"] = "Error occour when saving the sale order";
            }
            return RedirectToAction("List");
        }
        private int IsAlreadyExistInCart(string productId)
        {
            var cart = SessionHelper.GetDataFromSession<List<SaleItemViewModel>>(HttpContext.Session, "cart");
            for (var i = 0; i < cart.Count; i++) { 
                if (cart[i].ProductId.Equals(productId))
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult List()
        {
            var sale = _saleService.GetAll();
            return View(sale);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string Id)
        {
            _saleService.Delete(Id);
            return Json(new {success = true});
        }
        
    }
}
