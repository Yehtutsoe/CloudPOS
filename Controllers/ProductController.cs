using CloudPOS.Models;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Report.common;
using CloudPOS.Repositories.Report.ReportDataSet;
using CloudPOS.Services;
using CloudPOS.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _modelService;
        private readonly IReport _report;

        public ProductController(IProductService productService,ICategoryService categoryService,IBrandService modelService,IReport report)
        {
            _productService = productService;
            _categoryService = categoryService;
            _modelService = modelService;
            _report = report;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Entry()
        {
            return View(_productService.GetCategoryAndModel());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
            return View(_productService.GetAll());
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Update(ProductViewModel ui)
        {
            //if (!ModelState.IsValid)
            //{
            //    TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
            //    {
            //        Message = "Can not update the data,please check the input fields",
            //        IsOccurError = true
            //    });
            //    return View("Edit",ui);
            //}
            try
            {
                _productService.Update(ui);
                TempData["ErrorViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(new ErrorViewModel
                {
                    Message = "Successful update to system",
                    IsOccurError = false
                });
                //return RedirectToAction("List");
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
        public IActionResult Report()
        {
            ViewBag.Category = _categoryService.RetrieveAll();
            ViewBag.PhoneModel = _modelService.RetrieveAll();
            return View();
        }
        [HttpPost]
        public IActionResult Report(string productName,string modelId,string categoryId)
        {
            string fileDownloadName = $"productReport{Guid.NewGuid():N}.xlsx";
          // Console.WriteLine($"🔎 Searching with - Product Name: {productName}, ModelId: {modelId}, CategoryId: {categoryId}");

            IList<ProductReport> productReports  = _report.GetProductReportBy(productName, modelId, categoryId);
           //Console.WriteLine($"📊 Total Records Found: {productReports.Count}");
            if (productReports.Count > 0)
            {
                var fileContentInByte = ReportHelper.ExportToExcel(productReports, fileDownloadName);
                var contentType = "application/vnd.ms-excel";
                return File(fileContentInByte, contentType, fileDownloadName);
            }
            else
            {
                ViewBag.Info = "There is no data to export";
                ViewBag.Category = _categoryService.RetrieveAll();
                ViewBag.PhoneModel = _modelService.RetrieveAll();
                return View();
            }

            
        }
    }
}
