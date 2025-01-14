namespace CloudPOS.Models.ViewModels
{
    public class SaleItemViewModel
    {
        public string Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public Decimal UnitPrice { get; set; }
        public string SaleId { get; set; }
        public string ProductId { get; set; }
        public string ProductInfo { get; set; }
        public string Remark { get; set; }
        public IList<ProductViewModel> ProductViewModels { get; set; }
    }
}
