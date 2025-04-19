using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services.ReportingServices
{
    public interface IPurchaseDetailsReportService
    {
        IList<PurchaseDetailsReportViewModel> GetPruchaseReport(string fromDate, string toDate,string productId);
    }
}
