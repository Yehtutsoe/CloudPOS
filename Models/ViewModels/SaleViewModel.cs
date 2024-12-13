using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class SaleViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Sale Date is required")]
        public DateTime SaleDate { get; set; }
        [Required(ErrorMessage ="Total Amount is required")]
        public Decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Total Cost is required")]
        public Decimal TotalCost { get; set; }
        public Decimal Profit { get; set; }
        public IList<SaleItemViewModel> SaleItemViewModels { get; set; }
    }
}
