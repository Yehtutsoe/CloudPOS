namespace CloudPOS.Models.ViewModels
{
    public class PurchaseViewModel
    {
        public DateTime PurchaseDate { get; set; }
        public string SupplierId { get; set; }
        public string SupplierInfo { get; set; }
        public Decimal TotalCost { get; set; }
        public string? Deliverystatus { get; set; }
    }
}
