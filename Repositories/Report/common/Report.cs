using CloudPOS.Repositories.Report.ReportDataSet;
using CloudPOS.Services;

namespace CloudPOS.Repositories.Report.common
{
    public class Report : IReport
    {
        private readonly IProductService _productService;

        public Report(IProductService productService)
        {
            _productService = productService;
        }
        public IList<ProductReport> GetProductReportBy(string productName, string categoryId, string modelId)
        {
           var products = new List<ProductReport>();
            if (!string.IsNullOrEmpty(productName))
            {
                products = _productService.RetrieveAll().Where(w => w.Name == productName)
                                                        .Select(s => new ProductReport()
                                                        {
                                                            Name = s.Name,
                                                            CostPrice = s.CostPrice,
                                                            Quantity = s.Quantity,
                                                            SalePrice = s.SalePrice,
                                                            IMEINumber = s.IMEINumber,
                                                            SerialNumber = s.SerialNumber,
                                                            ModelInfo = s.PhoneModelInfo,
                                                            CategoryInfo = s.CategoryInfo,
                                                            ReportedAt = DateTime.Now

                                                        }).ToList();
            }
            else if (modelId != "a" || categoryId != "a")
            {
                products = _productService.RetrieveAll().Where(w => w.CategoryId == categoryId || w.PhoneModelId == modelId)
                                                        .Select(s => new ProductReport
                                                                {
                                                                    Name = s.Name,
                                                                    CostPrice = s.CostPrice,
                                                                    Quantity = s.Quantity,
                                                                    SalePrice = s.SalePrice,
                                                                    IMEINumber = s.IMEINumber,
                                                                    SerialNumber = s.SerialNumber,
                                                                    ModelInfo = s.PhoneModelInfo,
                                                                    CategoryInfo = s.CategoryInfo,
                                                                    ReportedAt = DateTime.Now
                                                                 }).ToList();

                 
            }

    }
}
