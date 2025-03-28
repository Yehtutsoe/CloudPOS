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
        public string? Description { get; set; }
        public string BarCode { get; set; }
        public string BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public BrandEntity Brands { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Categories { get; set; }
    }
 
}
