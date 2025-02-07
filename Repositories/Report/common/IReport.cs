using CloudPOS.Repositories.Report.ReportDataSet;

namespace CloudPOS.Repositories.Report.common
{
    public interface IReport
    {
        IList<ProductReport> GetProductReportBy(string productName, string categoryId, string modelId);
    }
}
