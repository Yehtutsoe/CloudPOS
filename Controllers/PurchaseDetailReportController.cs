using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using CloudPOS.Services.ReportingServices;
using CloudPOS.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class PurchaseDetailReportController : Controller
    {
        private readonly IPurchaseDetailsReportService _detailsReportService;
        private readonly IProductService _productService;


        public PurchaseDetailReportController(IPurchaseDetailsReportService detailsReportService,
                                              IProductService productService)
        {
            this._detailsReportService = detailsReportService;
            this._productService = productService;

        }
        public IActionResult Report()
        {
            LoadDataBinding();
            return View();
        }
        [HttpPost]
        public IActionResult Report(string FromDate,string ToDate,string ProductId,bool IsDownload = false)
        {
            LoadDataBinding();
            IList<PurchaseDetailsReportViewModel> purchaseDetails = _detailsReportService.GetPruchaseReport(FromDate, ToDate, ProductId);
            string downloadFileName = $"pruchaseDetails_{DateTime.Now.ToString()}.xlsx";

            if (IsDownload && purchaseDetails.Any())
            {
                var totalQuantity = purchaseDetails.Sum(pd => pd.Quantity);
                var totalPrice = purchaseDetails.Sum(pd => pd.Price);
                var totalAmount = purchaseDetails.Sum(pd => pd.TotalPrice);
                purchaseDetails.Add(new PurchaseDetailsReportViewModel
                {
                    Quantity = totalQuantity,
                    Price = totalPrice,
                    TotalPrice = totalAmount

                });
                var fileContentByte = ReportHelper.ExportToExcel(purchaseDetails, downloadFileName);
                var contentType = "application/vnd.openxmlformat-officedocument.spreadsheet.sheet";
                ViewData["Info"] = "Successfully download the purchasedeatil ExcelFile.";
                ViewData["Status"] = true;
                return File(fileContentByte, contentType, downloadFileName);
            }
            else
            {

                ViewData["Info"] = purchaseDetails.Any() ? "Report loaded successfully." : "No Purchase Details found.";
                ViewData["Status"] = purchaseDetails.Any();
                ViewBag.ReportData = purchaseDetails;
                ViewBag.FromDate = FromDate;
                ViewBag.ToDate = ToDate;
                ViewBag.ProductId = ProductId;
                return View();
            }
        }

        private void LoadDataBinding()
        {
            ViewBag.Products = _productService.GetProducts();
           // ViewBag.Suppliers = _supplierService.GetSuppliers();

        }
    }
}
