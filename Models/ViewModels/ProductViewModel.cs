namespace CloudPOS.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string SerialNumber { get; set; }
        public string IMEINumber { get; set; }
        public Decimal SalePrice { get; set; }
        public Decimal CostPrice { get; set; }
        public string PhoneModelId { get; set; }
        public string PhoneModelInfo { get; set; }
        public IList<PhoneModelViewModel> PhoneModelViewModels { get; set; } //List for UI Binding
        public string CategoryId { get; set; }
        public string CategoryInfo { get; set; }
        public IList<CategoryViewModel> CategoryViewModels { get; set; } // List for UI Binding
        
    }
}
