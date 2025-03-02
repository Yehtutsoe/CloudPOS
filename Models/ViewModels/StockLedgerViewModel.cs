namespace CloudPOS.Models.ViewModels
{
    public class StockLedgerViewModel 
    {
        public string id { get; set; }
        public DateTime? LedgerDate { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductId { get; set; }
        public string TransactionType { get; set; }
        public int InQty { get; set; }
        public int OutQty { get; set; }
        public int Quantity { get; set; }

    }
}
