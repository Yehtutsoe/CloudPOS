using CloudPOS.Models;
using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudPOS.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        #region Constructor
        public BrandController(IBrandService modelService,ICategoryService categoryService)
        {
            _brandService = modelService;
            _categoryService = categoryService;
        }
        #endregion

        #region Create
        [Authorize(Roles = "Admin")]
        public IActionResult Entry()
        {
            var nextCode = _brandService.GetLastBrandCode();
            var brand = new BrandViewModel()
            {
                Code = nextCode,
                Name = string.Empty,
                CategoryId = string.Empty
            };
            BindCategory();
            return View(brand);
        }

        private void BindCategory()
        {
            var categorys = _categoryService.GetCategories().Select(c => new SelectListItem() { Value = c.Id, Text = c.Name }).ToList();
           ViewBag.Categorys = categorys;
        }
    
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Entry(BrandViewModel brandViewModel)
        {
            try
            {
                var isExist = _brandService.IsAlreadyExist(brandViewModel);
                if (isExist)
                {
                    ViewData["Info"] = "This Brand Name is already exist";
                    ViewData["Status"] = false;
                    return View(brandViewModel);
                }
                _brandService.Create(brandViewModel);
                ViewData["Info"] = "Create successfully";
                ViewData["Status"] = true;
            }
            catch(Exception ex)
            {
                ViewData["Info"] = "This Brand is not saved to system" + ex.Message;
                ViewData["Status"] = false;
            }
            
            return RedirectToAction("List");
        }
        #endregion

        #region Retrieve
        public IActionResult List()
        {
            return View(_brandService.GetAll());
        }
        #endregion

        #region Edit
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string Id)
        {
            var brand = _brandService.GetById(Id);
            if(brand == null)
            {
                TempData["Error"] = "Brand not found";
                return RedirectToAction("List");
            }
            BindCategory();
            return View(brand);
        }
        #endregion

        #region Update
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(BrandViewModel model) {

                try {
                    _brandService.Update(model);
                    ViewData["Info"] = "Successfully update the data";
                    ViewData["Status"] = true;
                }
                catch (Exception ex)
                {
                    ViewData["Info"] = "Can not update the data" + ex.Message;
                    ViewData["Status"] = false;
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
                _brandService.Delete(Id);
                ViewData["Info"] = "Deleted the record";
                ViewData["Status"] = true;
            }
            catch (Exception ex)
            {
                ViewData["Info"] = "Can not Delete the record" + ex.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }
        #endregion
    }
}
