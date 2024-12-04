using CloudPOS.Models;
using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
       
        #region Constructor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Entry
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(CategoryViewModel ui) {

            try
            {
                if (!ModelState.IsValid)
                {
                    _categoryService.Create(ui);
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Successfully save the record to the system",
                        IsOccurError = false
                    });
                }
                else
                {
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Error occour, can not save to the system",
                        IsOccurError = true
                    });
                }
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

        #region Retrieve
        public IActionResult List()
        {
            return View(_categoryService.RetrieveAll());
        }
        #endregion

        #region Edit
        public IActionResult Edit(string Id)
        {
           return View(_categoryService.GetById(Id));
        }
        #endregion

        #region Delete
        public IActionResult Delete(string Id)
        {
            try
            {
                    _categoryService.Delete(Id);
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Successfully delete the record from the system",
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

        #region Update
        public IActionResult Update(CategoryViewModel ui)
        {
          
                if (!ModelState.IsValid)
                {
                    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                    {
                        Message = "Validation Error occour, please check input field",
                        IsOccurError = true
                    });
                return View(ui);
                }
            try
            {
                _categoryService.Update(ui);
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = "Successful update to the system",
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
