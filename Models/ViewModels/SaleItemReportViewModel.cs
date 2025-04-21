namespace CloudPOS.Models.ViewModels
{
    public class SaleItemReportViewModel
    {
        public string SaleDate { get; set; }
        public string VoucherNo { get; set; }
        public string SaleType { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Amount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }

    }
}
