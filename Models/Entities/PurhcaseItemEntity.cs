using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("PurchaseItem")]
    public class PurhcaseItemEntity:BaseEntity
    {
        
        [MaxLength(10)]
        public int Quantity { get; set; }
        public Decimal CostPerUnit { get; set; }
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Products { get; set; }
        public string PurchaseId { get; set; }
        [ForeignKey(nameof(PurchaseId))]
        public PurchaseEntity Purchases { get; set; }
    }
}
