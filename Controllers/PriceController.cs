using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class PriceController : Controller
    {
        private readonly IPriceService _priceService;
        private readonly IProductService _productService;

        public PriceController(IPriceService priceService, IProductService productService)
        {
            this._priceService = priceService;
            this._productService = productService;
        }
        public IActionResult Entry()
        {
            BindingProduct();
            return View();
        }

        private void BindingProduct()
        {
            var products = _productService.GetProducts();
            ViewBag.Products = products;
        }
        [HttpPost]
        public IActionResult Entry(PriceViewModel model)
        {
            try
            {
                _priceService.Create(model);
                ViewData["Info"] = "Successfully save data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error occour  to save the data "+ e.Message;
                ViewData["status"] = false;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(string Id)
        {
            try
            {
                _priceService.Delete(Id);
                ViewData["Info"] = "Successfully delete data to the system";
                ViewData["status"] = true;
            }
            catch (Exception)
            {

                ViewData["Info"] = "Can not delete the data to the system";
                ViewData["status"] = false;
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id)
        {
            var entity = _priceService.GetById(Id);
            BindingProduct();
            return View(entity);
        }
        [HttpPost]
        public IActionResult Update(PriceViewModel model)
        {
            try
            {
                _priceService.Update(model);
                ViewData["Info"] = "Successfully update the data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Can not update data to the system" + e.Message;
                ViewData["status"] = false;
            }
            return RedirectToAction("List");
        }
    }
}
