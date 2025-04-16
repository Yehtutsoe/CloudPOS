using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("SaleItem")]
    public class SaleItemEntity:BaseEntity
    {
        
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public string ProductId { get; set; }
        public string SaleId { get; set; }
        public Decimal SaleAmount { get; set; }
    }
}
