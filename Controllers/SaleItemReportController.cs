using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using CloudPOS.Services.ReportingServices;
using CloudPOS.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class SaleItemReportController : Controller
    {
        private readonly ISaleItemReportService _saleItemReport;
        private readonly IProductService _productService;

        public SaleItemReportController(ISaleItemReportService saleItemReport,IProductService productService)
        {
            this._saleItemReport = saleItemReport;
            this._productService = productService;
        }
        public IActionResult Report()
        {
            ProductDataBinding();
            return View();
        }
        [HttpPost]
        public IActionResult Report(string FromDate,string ToDate,string ProductId,bool IsDownload = false)
        {
            ProductDataBinding();
            IList<SaleItemReportViewModel> saleItem = _saleItemReport.GetSaleItemReport(FromDate, ToDate, ProductId);
            var downloadFileName = $"SaleItemReport_{DateTime.Now}.xlsx";
            if(IsDownload && saleItem.Any())
            {
                var totalQuantity = saleItem.Sum(si => si.Quantity);
                var totalAmount = saleItem.Sum(si => si.Amount);
                var totalPrice = saleItem.Sum(si => si.Price);
                var total = saleItem.Sum(si => si.Total);
                saleItem.Add(new SaleItemReportViewModel
                {
                   Quantity = totalQuantity,
                   Amount = totalAmount, 
                   Price = totalPrice,
                   Total = total
                });
                var fileContentByte = ReportHelper.ExportToExcel(saleItem,downloadFileName);
                var contentType = "application/vnd.openxmlformat-officedocument.spreadsheet.sheet";
                ViewData["Info"] = "Successfully download the saleItem ExcelFile.";
                ViewData["Status"] = true;
                return File(fileContentByte, contentType, downloadFileName);
            }
            else
            {

                ViewData["Info"] =saleItem.Any() ? "Report loaded successfully." : "No Sale Item found.";
                ViewData["Status"] = saleItem.Any();
                ViewBag.ReportData = saleItem;
                ViewBag.FromDate = FromDate;
                ViewBag.ToDate = ToDate;
                ViewBag.ProductId = ProductId;
                return View();
            }
        }
        private void ProductDataBinding()
        {
            ViewBag.Products = _productService.GetProducts();
        }
    }
}
