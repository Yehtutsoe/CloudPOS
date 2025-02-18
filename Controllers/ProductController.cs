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
        private readonly IBrandService _brandService;
        private readonly IReport _report;

        public ProductController(IProductService productService,ICategoryService categoryService,IBrandService modelService,IReport report)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = modelService;
            _report = report;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Entry()
        {
            return View();
        }

        public IActionResult Report()
        {
            ViewBag.Category = _categoryService.RetrieveAll();
            ViewBag.PhoneModel = _brandService.RetrieveAll();
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
                ViewBag.PhoneModel = _brandService.RetrieveAll();
                return View();
            }
        }
    }
}
