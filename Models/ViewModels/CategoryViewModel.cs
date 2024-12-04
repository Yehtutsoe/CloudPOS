using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Desciption is required")]
        public string Description { get; set; }
    }
}
