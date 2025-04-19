namespace CloudPOS.Models.ViewModels
{
    public class ProductReportViewModel
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? BrandInfo { get; set; }
        public string? CategoryInfo { get; set; }
        public DateTime ReportedAt { get; set; }

    }
}
