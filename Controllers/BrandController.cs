using CloudPOS.Models;
using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _modelService;

        #region Constructor
        public BrandController(IBrandService modelService)
        {
            _modelService = modelService;
        }
        #endregion

        #region Create
        [Authorize(Roles = "Admin")]
        public IActionResult Entry()
        {
            return View();
        }
    
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Entry(BrandViewModel modelViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _modelService.Create(modelViewModel);
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
                        Message = "Error Occour, can not save the record",
                        IsOccurError = true
                    });
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = $"Error : {ex.Message}",
                    IsOccurError = true
                });
            }
            
            return RedirectToAction("List");
        }
        #endregion

        #region Retrieve
        public IActionResult List()
        {
            return View(_modelService.RetrieveAll());
        }
        #endregion

        #region Edit
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string Id)
        {
            return View(_modelService.GetById(Id));
        }
        #endregion

        #region Update
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(BrandViewModel model) {

                if (!ModelState.IsValid)
                {
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Successful update the record to the system",
                        IsOccurError = true
                    });
                }
                try {
                    _modelService.Update(model);
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Successful update the record to the system",
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
        #endregion

        #region Delete
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string Id)
        {
            try
            {
                    _modelService.Delete(Id);
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Successful delete the record to the system",
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
        #endregion
    }
}
