using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("StockIncome")]
    public class StockIncomeEntity:BaseEntity
    {
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public string? DeliveryStatus { get; set; }
        public string SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        public SupplierEntity Suppliers { get; set; }
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Products { get; set; }

    }
}
