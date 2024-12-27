using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Requried Type")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Requried Serial Number")]
        public string SerialNumber { get; set; }
        [Required(ErrorMessage = "IMEI Number is Requried")]
        public string IMEINumber { get; set; }
        [Range(0,double.MaxValue,ErrorMessage = "Sale Price must be positive number")]
        public Decimal SalePrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Cost Price must be positive number")]
        public int? Quantity { get; set; }
        public Decimal CostPrice { get; set; }
        [Required(ErrorMessage = "Phone Model is required")]
        public string PhoneModelId { get; set; }
        public string PhoneModelInfo { get; set; }
        public IList<PhoneModelViewModel> PhoneModelViewModels { get; set; } //List for UI Binding
        [Required(ErrorMessage = "Category is required")]
        public string CategoryId { get; set; }
        public string CategoryInfo { get; set; }
        public IList<CategoryViewModel> CategoryViewModels { get; set; } // List for UI Binding
        
    }
}
