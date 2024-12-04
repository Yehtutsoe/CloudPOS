using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class PhoneModelViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Speicification is required")]
        public string Specification { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
