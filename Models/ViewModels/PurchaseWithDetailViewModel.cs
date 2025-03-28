namespace CloudPOS.Models.ViewModels
{
    public class PurchaseWithDetailViewModel
    {

        public PurchaseViewModel Purchase { get; set; }
        public List<ProductDetailViewModel> ProductDetails { get; set; }


        public PurchaseWithDetailViewModel()
        {
            Purchase = new PurchaseViewModel();
            ProductDetails = new List<ProductDetailViewModel>();
        }
    }
    public class PurchaseViewModel
     {
        public string Id { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Today;
        public string VoucherNo { get; set; }
        public string SupplierId { get; set; }
        public string SupplierInfo { get; set; }
        public Decimal TotalAmount { get; set; } = 0;
        public string? Deliverystatus { get; set; }

        public PurchaseViewModel() { }
    }

    public class ProductDetailViewModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime EarliestDate { get; set; }

        // Parameterless constructor
        public ProductDetailViewModel() { }
    }
}
