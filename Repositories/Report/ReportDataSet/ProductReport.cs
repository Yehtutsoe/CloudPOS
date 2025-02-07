namespace CloudPOS.Repositories.Report.ReportDataSet
{
    public class ProductReport
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string IMEINumber { get; set; }
        public int Quantity { get; set; }
        public Decimal CostPrice { get; set; }
        public Decimal SalePrice { get; set; }
        public string? ModelInfo { get; set; }
        public string? CategoryInfo { get; set; }
        public DateTime ReportedAt { get; set; }

    }
}
