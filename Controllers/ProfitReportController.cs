using CloudPOS.Models.ViewModels;
using CloudPOS.Services.ReportingServices;
using CloudPOS.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class ProfitReportController : Controller
    {
        private readonly IProfitReportService _profitReportService;

        public ProfitReportController(IProfitReportService profitReportService)
        {
            this._profitReportService = profitReportService;
        }
        public IActionResult ProfitReport()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProfitReport(string fromDate, string toDate)
        {
            IList<ProfitReportViewModel> profitReport = _profitReportService.GetProfitReport(fromDate, toDate);
            string downloadFileName = $"profitReport_{DateTime.Now:yyyy-MM-dd}.xlsx";
            if (profitReport.Any())
            {
                var totalQuantitySold = profitReport.Sum(r => r.SoldQuantity);
                var OriginalSellingPrice = profitReport.Sum(r => r.OriginalSellPrice);
                var discountpercent = profitReport.Sum(r => r.DiscountPercentage);
                var discountamount = profitReport.Sum(r => r.DiscountAmount);
                var totalPurchasePrice = profitReport.Sum(r => r.PurchasePrice);
                var totalCost = profitReport.Sum(r => r.TotalCost);
                var totalNetSale = profitReport.Sum(r => r.NetSalePrice);
                var totalRevenue = profitReport.Sum(r => r.TotalRevenue);
                var totalProfit = profitReport.Sum(r => r.Profit);

                // Calculate averageProfitMargin safely
                decimal? averageProfitMargin = 0;
                if (profitReport.Any(r => r.TotalRevenue > 0))
                {
                    averageProfitMargin = profitReport.Sum(r => r.ProfitMargin * (r.TotalRevenue / profitReport.Sum(x => x.TotalRevenue)));
                }

                profitReport.Add(new ProfitReportViewModel
                {
                    CategoryName = "Total",
                    SoldQuantity = totalQuantitySold,
                    OriginalSellPrice = OriginalSellingPrice,
                    DiscountPercentage = discountpercent,
                    DiscountAmount = discountamount,
                    NetSalePrice = totalNetSale,
                    TotalRevenue = totalRevenue,
                    PurchasePrice = totalPurchasePrice,
                    TotalCost = totalCost,
                    Profit = totalProfit,
                    ProfitMargin = averageProfitMargin
                });

                var fileContentByte = ReportHelper.ExportToExcel(profitReport, downloadFileName);
                var contentType = "application/vnd.openxmlformat-officedocument.spreadsheet.sheet";
                ViewData["Info"] = "Successfully download the overdue ExcelFile.";
                ViewData["Status"] = true;
                return File(fileContentByte, contentType, downloadFileName);

            }
            else
            {
                ViewData["Info"] = "not profit report.";
                ViewData["Status"] = false;
                return View();
            }

        }
    }
}