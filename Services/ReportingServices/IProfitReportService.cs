using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services.ReportingServices
{
    public interface IProfitReportService
    {
        IList<ProfitReportViewModel> GetProfitReport(string fromDate ,  string toDate);
    }
}
