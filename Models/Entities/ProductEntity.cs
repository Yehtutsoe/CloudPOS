using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Product")]
    public class ProductEntity:BaseEntity
    {
        [MaxLength(50)]      
        public string Type { get; set; }
        [MaxLength(50)]
        public string SerialNumber { get; set; }
        [MaxLength(18)]      
        public string IMEINumber { get; set; }
        public Decimal CostPrice { get; set; }
        public Decimal SalePrice { get; set; }
        public string PhoneModelId { get; set; }
        [ForeignKey(nameof(PhoneModelId))]
        public PhoneModelEntity PhoneModels { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Categories { get; set; }
    }
 
}
