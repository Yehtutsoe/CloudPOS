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
            var query = _productService.GetAll().AsQueryable();

            // Filter by product name
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(w => w.Name == productName);
            }

            // Filter by category and model
            if (!string.IsNullOrEmpty(categoryId) && categoryId != "a")
            {
                query = query.Where(w => w.CategoryId.Trim() == categoryId.Trim());
            }
            if (!string.IsNullOrEmpty(modelId) && modelId != "a")
            {
                query = query.Where(w => w.PhoneModelId.Trim() == modelId.Trim());
            }

            var products = query.Select(s => new ProductReport
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

            return products;
        }

    }
}

