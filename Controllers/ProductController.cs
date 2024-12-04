using CloudPOS.Models;
using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        

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
                if (!ModelState.IsValid) {
                    _productService.Create(ui);
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Successful save the record to the system",
                        IsOccurError = false
                    });
                }
                else
                {
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Error occour,unsuccessful save the record to the system",
                        IsOccurError = true
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = $"Error : {ex.Message}",
                    IsOccurError = false
                });
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
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = "Not Found the data",
                    IsOccurError = true
                });
                return RedirectToAction("List");
            }
            return View(product);
        }

        public IActionResult Delete(string Id)
        {
            try
            {
                _productService.Delete(Id);
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = "Successful Delete the record from the system",
                    IsOccurError = false
                });
            }
            
            catch (Exception ex)
            {
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = $"Error ,{ex.Message}",
                    IsOccurError = true
                });
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Update(ProductViewModel ui)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = "Can not update the data,please check the input fields",
                    IsOccurError = true
                });
                return View(ui);
            }
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
