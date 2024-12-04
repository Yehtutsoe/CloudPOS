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
                _categoryService.Create(ui);
                error.Message = "Successful save the record to the system";
                
            }
            catch (Exception)
            {
                error.Message = "Error occour, can not save the record";
                error.IsOccurError = true;
                throw;
            }
            ViewBag.Msg = error;
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
                TempData["Msg"] = "Successfully Delete from the System";
                TempData["IsOccourError"] = false;
            }
            catch (Exception)
            {
                TempData["Msg"] = "Error Occour,unsuccessfully Delete from the System";
                TempData["IsOccourError"] = true;
                throw;
            }
            return RedirectToAction("List");
        }
        #endregion

        #region Update
        public IActionResult Update(CategoryViewModel ui)
        {
            try
            {
                _categoryService.Update(ui);
                TempData["Msg"] = "Successfully Update to the System";
                TempData["IsOccourError"] = false;
            }
            catch (Exception)
            {
                TempData["Msg"] = "Error Occour,unsuccessfully Update to the System";
                TempData["IsOccourError"] = true;
                throw;
            }
            return RedirectToAction("List");
        }
        #endregion
    }
}
