using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Purchase")]
    public class PurchaseEntity:BaseEntity
    {
        public DateTime PurchaseDate { get; set; }
        public string VoucherNo { get; set; }
        public string SupplierId { get; set; }
        public Decimal TotalCost { get; set; }
        public string? Deliverystatus { get; set; }
        public virtual ICollection<PurchaseDetailEntity> PurchaseDetails { get; set; } = new List<PurchaseDetailEntity>();

    }
}
