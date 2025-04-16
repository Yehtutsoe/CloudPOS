using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("StockLedger")]
    public class StockLedgerEntity:BaseEntity
    {
        public DateTime LedgerDate { get; set; }
        public DateTime EarliestDate { get; set; }
        public string SourceId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; }
    }
}
