using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Inventory")]
    public class InventoryEntity : BaseEntity
    {
        public int Quantity { get; set; } 

        public DateTime EarliestDate { get; set; }

        [Required]
        public string ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductEntity Products { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Categorys { get; set; }
    }
}
