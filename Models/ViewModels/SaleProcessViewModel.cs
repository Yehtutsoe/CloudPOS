using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class SaleProcessViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Sale Date is required")]
        public DateTime SaleDate { get; set; }
        public Decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public string ProductInfo { get; set; }

    }
}
