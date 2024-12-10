using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class PurchaseViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Date and Time is required")]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage ="Total Cost is required")]
        public Decimal TotalCost { get; set; }
        [Required(ErrorMessage ="Delivery Status is required")]
        public string DeliveryStatus { get; set; }
        public string SupplierId { get; set; }
        public string SupplierInfo { get; set; }
        public IList<SupplierViewModel> SupplierViewModels { get; set; }
    }
}
