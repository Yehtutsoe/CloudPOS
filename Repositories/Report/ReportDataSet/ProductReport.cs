namespace CloudPOS.Repositories.Report.ReportDataSet
{
    public class ProductReport
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public Decimal? CostPrice { get; set; }
        public Decimal? SalePrice { get; set; }
        public Decimal? TotalAmount { get; set; }
        public string? BrandInfo { get; set; }
        public string? CategoryInfo { get; set; }
        public DateTime ReportedAt { get; set; }

    }
}
