using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        ErrorViewModel error = new ErrorViewModel();

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Entry()
        {
            return View(_productService.GetCategoryAndModel());
        }
        [HttpPost]
        public IActionResult Entry(ProductViewModel ui)
        {
            try
            {
                _productService.Create(ui);
                error.Message = "Successfull save the record to system";
                
            }
            catch (Exception)
            {
                error.Message = "Error occour,Unsuccessfull save the record to system";
                error.IsOccurError = true;
                throw;
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            return View(_productService.RetrieveAll());
        }

        public IActionResult Edit(string Id)
        {
            var product = _productService.GetById(Id);
            if(product == null)
            {
                TempData["Msg"] = "Error: Product not found!";
                TempData["IsOccourError"] = true;
                return RedirectToAction("List");
            }
            return View(product);
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                _productService.Delete(Id);
                TempData["Msg"] = "Successfully Delete from the System";
                TempData["IsOccourError"] = false;
            }
            catch (Exception)
            {
                TempData["Msg"] = "Error occour,Unsuccessfully Delete from the System";
                TempData["IsOccourError"] = true;
                throw;
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Update(ProductViewModel ui)
        {
            try
            {
                _productService.Update(ui);
                TempData["Msg"] = "Successfully update to the System";
                TempData["IsOccourError"] = false;
            }
            catch (Exception)
            {
                TempData["Msg"] = "Error occour,Unsuccessfully update to the System";
                TempData["IsOccourError"] = true;
                throw;
            }
            return RedirectToAction("List");
        }
    }
}
