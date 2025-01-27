using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class StockIncomeViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Date and Time is required")]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage ="Total Cost is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage ="Delivery Status is required")]
        public string DeliveryStatus { get; set; }
        public string ProductId { get; set; }
        public string  ProductInfo { get; set; }
        public string SupplierId { get; set; }
        public string SupplierInfo { get; set; }
        public IList<SupplierViewModel> SupplierViewModels { get; set; } = new List<SupplierViewModel>();
        public IList<ProductViewModel> ProductViewModels { get; set; } = new List<ProductViewModel>();
    }
}
