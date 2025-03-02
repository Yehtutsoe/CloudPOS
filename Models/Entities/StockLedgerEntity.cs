using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("StockLedger")]
    public class StockLedgerEntity:BaseEntity
    {
        public DateTime LedgerDate { get; set; }
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))] 
        public ProductEntity Products { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; }
    }
}
