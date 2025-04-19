namespace CloudPOS.Models.ViewModels
{
    public class PurchaseDetailsReportViewModel
    {
        public string PurchaseDate { get; set; }
        public string PurchaseVoucherNo { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string SupplierName { get; set; }

    }
}
