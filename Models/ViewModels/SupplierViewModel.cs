using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class SupplierViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Required Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Required Contact Information")]
        public string ContactInformation { get; set; }
        [Required(ErrorMessage ="Required Address")]
        public string Address { get; set; }
    }
}
