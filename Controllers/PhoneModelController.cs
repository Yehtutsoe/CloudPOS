using CloudPOS.Models;
using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class PhoneModelController : Controller
    {
        private readonly IPhoneModelService _modelService;

        #region Constructor
        public PhoneModelController(IPhoneModelService modelService)
        {
            _modelService = modelService;
        }
        #endregion

        #region Create
        public IActionResult Entry()
        {
            return View();
        }
    
        [HttpPost]
        public IActionResult Entry(PhoneModelViewModel modelViewModel)
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
        public IActionResult Edit(string Id)
        {
            return View(_modelService.GetById(Id));
        }
        #endregion

        #region Update
        [HttpPost]
        public IActionResult Update(PhoneModelViewModel model) {

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
