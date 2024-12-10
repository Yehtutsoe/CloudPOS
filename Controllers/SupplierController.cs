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
            return View();
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
            return View(_supplierService.RetrieveAll()); 
        }
        public IActionResult Delete(string Id)
        {
            try
            {
                _supplierService.Delete(Id);
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = "Successfully delete the record to the System",
                    IsOccurError = false
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = $"Error : {ex.Message} ",
                    IsOccurError = true
                });
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
            if (!ModelState.IsValid)
            {
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = "Can not update the record to the System",
                    IsOccurError = true
                });
            }
            try
            {
                _supplierService.Update(ui);
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = "Successfully update the record to the System",
                    IsOccurError = false
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = $"Error : {ex.Message}",
                    IsOccurError = true
                });
            }
            return RedirectToAction("List");
        }
    }
}
