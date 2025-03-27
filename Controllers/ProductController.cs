using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Report.common;
using CloudPOS.Repositories.Report.ReportDataSet;
using CloudPOS.Services;
using CloudPOS.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudPOS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IPriceService _priceService;
        private readonly IReport _report;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 IBrandService modelService,
                                 IPriceService priceService,
                                 IReport report)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = modelService;
            _priceService = priceService;
            _report = report;
        }
        [HttpGet]
        public ActionResult GetBrand(string CategoryId)
        {
            var brands = _brandService.GetBrandsByCategory(CategoryId);
            return Json(brands);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Entry()
        {
            string nextCode = _productService.GetNextProductCode();

            ProductViewModel productViewModel = new ProductViewModel
            {
                Code = nextCode,
                Name =string.Empty,
                CategoryId =string.Empty,
                BrandId =string.Empty,
                CostPrice = 0.00m,
                SalePrice = 0.00m,
                Description =string.Empty,
                Quantity = 0,
                BrandInfo = string.Empty,
                CategoryInfo = string.Empty
            };
            PriceViewModel priceViewModel = new PriceViewModel
            {
                PurchasePrice = 0.00m,
                RetailSalePrice = 0.00m,
                WholeSalePrice = 0.00m
            };
            ViewData["Info"] = null;
            ViewData["Status"] = null;
            BindBrandData();
            BindCategroyData();
            return View(Tuple.Create(productViewModel,priceViewModel));
        }
        private void BindBrandData()
        {
            var brands = _brandService.GetBrands().Select(b => new SelectListItem() { Value = b.Id, Text = b.Name }).ToList();
            ViewBag.Brands = brands ?? new List<SelectListItem>();
        }
        private void BindCategroyData()
        {
            var category = _categoryService.GetCategories().Select(c => new SelectListItem() { Value = c.Id, Text = c.Name }).ToList();
            ViewBag.Categorys = category ?? new List<SelectListItem>();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult Entry(ProductViewModel productViewModel)
        {
            try
            {
                var alreadyExist = _productService.IsAlreadyExist(productViewModel);
                if (alreadyExist)
                {
                    ViewData["Info"] = "Product is Already exist in Database";
                    ViewData["Status"] = false;
                    BindBrandData();
                    BindCategroyData();
                    return View(productViewModel);
                }
                _productService.Create(productViewModel);
                ViewData["Info"] = "Successfull save the record";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occour when saving the data" + e.Message;
                ViewData["Status"] = false;
            }
            BindBrandData();
            BindCategroyData();
            return RedirectToAction("List");
        }
        public IActionResult Report()
        {
            ViewBag.Category = _categoryService.GetAll();
            ViewBag.PhoneModel = _brandService.GetAll();
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
                ViewBag.Category = _categoryService.GetAll();
                ViewBag.PhoneModel = _brandService.GetAll();
                return View();
            }
        }
        public IActionResult List()
        {
            var products = _productService.GetAll();
            return View(products);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(string Id)
        {
            var itemView = _productService.GetById(Id);
            BindBrandData();
            BindCategroyData();
            return View(itemView);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult Update(ProductViewModel productViewModel)
        {
            try
            {
                _productService.Update(productViewModel);
                TempData["Info"] = "Successfully update the data ";
                TempData["Status"] = true;
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occour when update the data " + e.Message;
                TempData["Status"] = false;
            }
            return RedirectToAction("List");
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(string Id)
        {
            try
            {
                _productService.Delete(Id);
                TempData["Info"] = "Successfully delete the data";
                TempData["Status"] = true;
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occour when delete the data";
                TempData["Satatus"] = false;
            }
            return RedirectToAction("List");
        }
        public IActionResult GetBrandByCategory(string categoryId)
        {
            var brands = _brandService.GetBrandsByCategory(categoryId)
                                      .Select(s => new SelectListItem() { Value = s.Id, Text = s.Name }).ToList();
            return Json(brands);
        }
    }
}
