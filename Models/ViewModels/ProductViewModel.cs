using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Requried Type")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Requried Serial Number")]
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "IMEI Number is Requried")]
        public string Description { get; set; }
        [Range(0,double.MaxValue,ErrorMessage = "Sale Price must be positive number")]
        public Decimal SalePrice { get; set; }
        public Decimal DiscountPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Cost Price must be positive number")]
        public int Quantity { get; set; }
        public Decimal CostPrice { get; set; }
        [Required(ErrorMessage = "Phone Model is required")]
        public string BrandId { get; set; }
        public string BrandInfo { get; set; }
        public IList<BrandViewModel> BrandViewModels { get; set; } //List for UI Binding
        [Required(ErrorMessage = "Category is required")]
        public string CategoryId { get; set; }
        public string CategoryInfo { get; set; }
        public IList<CategoryViewModel> CategoryViewModels { get; set; } // List for UI Binding
        
    }
}
