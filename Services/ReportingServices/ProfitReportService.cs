using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CloudPOS.Services.ReportingServices
{
    public class ProfitReportService : IProfitReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProfitReportService> _logger;

        public ProfitReportService(IUnitOfWork unitOfWork, ILogger<ProfitReportService> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public IList<ProfitReportViewModel> GetProfitReport(string fromDate, string toDate)
        {
            DateTime? fromDateValue = null;
            DateTime? toDateValue = null;

            if (!string.IsNullOrEmpty(fromDate)) fromDateValue = DateTime.Parse(fromDate);
            if (!string.IsNullOrEmpty(toDate)) toDateValue = DateTime.Parse(toDate);

            var profitData = (from si in _unitOfWork.SaleItems.GetAll()
                              join s in _unitOfWork.Sales.GetAll()
                              on si.SaleId equals s.Id
                              join p in _unitOfWork.Products.GetAll()
                              on si.ProductId equals p.Id
                              join c in _unitOfWork.Categories.GetAll()
                              on p.CategoryId equals c.Id
                              where (fromDateValue == null || s.SaleDate >= fromDateValue) &&
                                    (toDateValue == null || s.SaleDate <= toDateValue)
                              select new
                              {
                                  si,
                                  s,
                                  p,
                                  c
                              })
                              .ToList() // Bring data into memory for complex calculations
                              .Select(x =>
                              {
                                  _logger.LogInformation($"Processing SaleItem: ProductId = {x.p.Id}, SaleDate = {x.s.SaleDate}");

                                  // Find the most recent PurchaseDetail before the SaleDate
                                  var purchaseDetailQuery = _unitOfWork.PurchaseDetails.GetAll()
                                      .Where(pd => pd.ProductId == x.p.Id && pd.EarliestDate <= x.s.SaleDate && pd.EarliestDate != DateTime.MinValue) // Exclude PurchaseDetails with EarliestDate = DateTime.MinValue
                                      .OrderByDescending(pd => pd.EarliestDate);

                                  string sql;

#if EF_CORE
                                      sql = purchaseDetailQuery.ToQueryString();
#else
                                  try
                                  {
                                     // sql = ((IObjectContextAdapter)_unitOfWork.GetDbContext()).ObjectContext.CreateQuery(purchaseDetailQuery.ToString()).ToTraceString();
                                  }
                                  catch (Exception ex)
                                  {
                                      _logger.LogError($"Error getting SQL query: {ex.Message}");
                                      sql = "Unable to retrieve SQL query.";
                                  }
#endif

                                 // _logger.LogInformation($"PurchaseDetail SQL: {sql}"); // Log the SQL

                                  // Log all PurchaseDetail records being considered
                                  foreach (var pd in _unitOfWork.PurchaseDetails.GetAll().Where(pd => pd.ProductId == x.p.Id))
                                  {
                                      _logger.LogInformation($"Considering PurchaseDetail: ProductId = {pd.ProductId}, EarliestDate = {pd.EarliestDate}, SaleDate = {x.s.SaleDate}");
                                  }

                                  var purchaseDetail = purchaseDetailQuery.FirstOrDefault();

                                  if (purchaseDetail == null)
                                  {
                                      _logger.LogWarning($"No PurchaseDetail found for ProductId = {x.p.Id} and SaleDate = {x.s.SaleDate}");
                                  }
                                  else
                                  {
                                      _logger.LogInformation($"Found PurchaseDetail: ProductId = {purchaseDetail.ProductId}, EarliestDate = {purchaseDetail.EarliestDate}, Price = {purchaseDetail.Price}");
                                  }

                                  decimal purchasePrice = purchaseDetail?.Price ?? 0; // Default to 0 if no purchase found

                                  decimal discountPercentage = x.s.DiscountPercent ?? 0; // Handle nullable DiscountPercent
                                  decimal? discountAmount = x.si.DiscountAmount * discountPercentage / 100;
                                  decimal netSalePrice = x.si.Price - (x.si.Price * discountPercentage / 100);
                                  decimal totalRevenue = netSalePrice * x.si.Quantity;
                                  decimal totalCost = purchasePrice * x.si.Quantity;
                                  decimal profit = totalRevenue - totalCost;

                                  // Avoid division by zero
                                  decimal profitMargin = totalRevenue == 0 ? 0 : (profit / totalRevenue) * 100;

                                  return new ProfitReportViewModel
                                  {
                                      SaleDate = x.s.SaleDate.ToString("yyyy-MM-dd"),
                                      InvoiceNumber = x.s.VoucherNo,
                                      CategoryName = x.c.Name,
                                      ProductName = x.p.Name,
                                      SoldQuantity = x.si.Quantity,
                                      OriginalSellPrice = x.si.Price,
                                      PurchasePrice = purchasePrice,
                                      DiscountPercentage = discountPercentage,
                                      DiscountAmount = discountAmount,
                                      NetSalePrice = netSalePrice,
                                      TotalRevenue = totalRevenue,
                                      TotalCost = totalCost,
                                      Profit = profit,
                                      ProfitMargin = profitMargin
                                  };
                              });

            return profitData.ToList();
        }
    }
}