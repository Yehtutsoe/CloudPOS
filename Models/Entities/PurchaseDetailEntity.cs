using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("PurchaseDetail")]
    public class PurchaseDetailEntity:BaseEntity
    {
        public int Quantity { get; set; }
        public string PurchaseId { get; set; }
        public virtual PurchaseEntity Purchase { get; set; }
        public string ProductId { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal Price { get; set; }
        public DateTime EarliestDate { get; set; }
    }
}
