namespace CloudPOS.Models.ViewModels
{
    public class SaleItemsViewModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Decimal Total { get; set; }
        public Decimal Amount { get; set; }
        public string CategoryId { get; set; }
        public string SaleId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
