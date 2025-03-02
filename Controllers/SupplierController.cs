using CloudPOS.Models;
using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        public IActionResult Entry()
        {
            var nextCode = _supplierService.GetNextSupplierCode();
            var supplier = new SupplierViewModel()
            {
                Code = nextCode,
                Name = string.Empty,
                Address = string.Empty,
                ContactInformation = string.Empty
            };
            ViewData["Info"] = null;
            ViewData["Status"] = null;
            return View(supplier);
        }
        #region Create
        [HttpPost]
        public IActionResult Entry(SupplierViewModel supplier) 
        {
            try
            {
                if (!ModelState.IsValid) {
                    _supplierService.Create(supplier);
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Successfully save the record to the System",
                        IsOccurError = false
                    });
                }
                else
                {
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Validation Error Occour ",
                        IsOccurError = true
                    });
                }
                              
            }
            catch (Exception ex)
            {
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = $"Error,{ex.Message}",
                    IsOccurError = true
                });
                throw;
            }
            return RedirectToAction("List");
        }
        #endregion

        public IActionResult List() {
            return View(_supplierService.GetAll()); 
        }
        public IActionResult Delete(string Id)
        {
            try
            {
                _supplierService.Delete(Id);
                ViewData["Info"] = "Successfully save the record";
                ViewData["Status"] = true;
            }
            catch (Exception ex)
            {
                ViewData["Info"] = "Occour error to save the record" + ex.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(string Id)
        {
             return View(_supplierService.GetById(Id));     
        }

        [HttpPost]
        public IActionResult Update(SupplierViewModel ui)
        {
            
            try
            {
                _supplierService.Update(ui);
                ViewData["Info"] = "Successfully update the record";
                ViewData["Status"] = true;
            }
            catch (Exception ex)
            {
                ViewData["Info"] = "Unsuccessfully update the record" + ex.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }
    }
}
