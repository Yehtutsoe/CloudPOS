using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("SaleItem")]
    public class SaleItemEntity:BaseEntity
    {
        
        [MaxLength(20)]
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal DiscountAmount { get; set; }
        [Required]
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }
        public string SaleId { get; set; }
        [ForeignKey(nameof(SaleId))]
        public SaleEntity Sales { get; set; }
        public Decimal SaleAmount { get; set; }
    }
}
