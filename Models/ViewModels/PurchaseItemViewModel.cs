namespace CloudPOS.Models.ViewModels
{
    public class PurchaseItemViewModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public Decimal CostPerUnit { get; set; }
        public string PurchaseId { get; set; }
        public string PurchaseInfo { get; set; }
        public string ProductId { get; set; }
        public string ProductInfo { get; set; }
        public string SupplierId { get; set; }
        public string SupplierInfo { get; set; }
        public IList<ProductViewModel> ProductViewModels { get; set; }
    }
}
