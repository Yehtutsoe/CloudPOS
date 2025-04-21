using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services.ReportingServices
{
    public interface ISaleItemReportService
    {
        IList<SaleItemReportViewModel> GetSaleItemReport(string fromDate, string toDate ,string productId);
    }
}
