using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Product")]
    public class ProductEntity:BaseEntity
    {
        [MaxLength(50)]      
        public string Name { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [StringLength(450)]      
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public Decimal CostPrice { get; set; }
        public Decimal SalePrice { get; set; }
        public Decimal DiscountPrice { get; set; }
        public string BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public BrandEntity Brands { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Categories { get; set; }
    }
 
}
