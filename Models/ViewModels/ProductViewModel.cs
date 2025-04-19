using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string BrandId { get; set; }
        public string BrandInfo { get; set; }
        public string BarCode { get; set; }
        public string CategoryId { get; set; }
        public string CategoryInfo { get; set; }

        
    }
}
