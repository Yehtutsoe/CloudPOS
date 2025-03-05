namespace CloudPOS.Models.ViewModels
{
    public class PriceViewModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime PricingDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailSalePrice { get; set; }
        public decimal WholeSalePrice { get; set; }

    }
}
