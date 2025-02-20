using CloudPOS.Models;
using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

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
        [Authorize(Roles ="Admin")]
        public IActionResult Entry()
        {
            string nextCode = _categoryService.GetNextCategoryCode();
            var category = new CategoryViewModel()
            {
               Code = nextCode,
               Name = string.Empty,
               Description = string.Empty,

            };
            ViewData["Info"] = null;
            ViewData["Status"] = null;
            return View(category);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Entry(CategoryViewModel ui) {

            try
            {
                var isAlreadyExist = _categoryService.IsAlreadyExist(ui);
                if (isAlreadyExist)
                {
                    ViewData["Info"] = "Already exist this category";
                    ViewData["Status"] = false;
                    return View(ui);
                }
                _categoryService.Create(ui);
                ViewData["Info"] = "Successful save the record";
                ViewData["Status"] = true;
                return RedirectToAction("List");
    }
            catch (Exception e)
            {

                ViewData["Info"] = "Unsuccessful the recode to save";
                ViewData["Status"] = false;
            };

            return RedirectToAction("List");
        }

        #endregion

        #region Retrieve
        public IActionResult List()
        {
            return View(_categoryService.GetAll());
        }
        #endregion

        #region Edit
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string Id)
        {
           return View(_categoryService.GetById(Id));
        }
        #endregion

        #region Delete
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string Id)
        {
            try
            {
                _categoryService.Delete(Id);
                ViewData["Info"] = "Deleted the record";
                ViewData["Status"] = true;
            }
            catch (Exception ex)
            {
                ViewData["Info"] = "Error occour to Delete the record";
                ViewData["Status"] = true;
            }
            return RedirectToAction("List");
        }
        #endregion

        #region Update
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(CategoryViewModel ui)
        {
            try
            {
                _categoryService.Update(ui);
                ViewData["Info"] = "Update the successfully";
                ViewData["Status"] = true;
            }
            catch (Exception ex)
            {
                ViewData["Info"] = "error occour to update the record" + ex.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }
        #endregion
    }
}
