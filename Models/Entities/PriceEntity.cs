using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Price")]
    public class PriceEntity:BaseEntity
    {
        public string ProductId { get; set; }
        public DateTime PricingDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailSalePrice { get; set; }
        public decimal WholeSalePrice { get; set; }
    }
}
