using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class BrandViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Code is required")]
        public string Code { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
