using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services.Report
{
    public interface IProductReportService
    {
        IList<ProductReportViewModel> GetProductReportBy(string productName, string categoryId, string modelId);
    }
}
