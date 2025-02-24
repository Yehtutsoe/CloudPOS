using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Inventory")]
    public class InventoryEntity:BaseEntity
    {
        
        [MaxLength(15)]
        public int Quantity { get; set; }
        [MaxLength(15)]
        public DateTime AdjustmentDate { get; set; }
        [MaxLength(35)]
        public string? Reason { get; set; }
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Products { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Categorys { get; set; }
    }
}
