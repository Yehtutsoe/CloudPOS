namespace CloudPOS.Models.ViewModels
{
    public class ProfitReportViewModel
    {
        public string SaleDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int SoldQuantity { get; set; }
        public decimal OriginalSellPrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? NetSalePrice { get; set; }
        public decimal? TotalRevenue { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? Profit { get; set; }
        public decimal? ProfitMargin { get; set; }

    }
}
