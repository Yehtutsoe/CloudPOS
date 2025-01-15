using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Purchase")]
    public class PurchaseEntity:BaseEntity
    {
        public DateTime PurchaseDate { get; set; }
        public Decimal TotalCost { get; set; }
        public string? DeliveryStatus { get; set; }
        public string SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        public SupplierEntity Suppliers { get; set; }
        public IList<PurchaseItemEntity> PurchaseItems { get; set; } = new List<PurchaseItemEntity>();
    }
}
