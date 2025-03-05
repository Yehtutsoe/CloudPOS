namespace CloudPOS.Models.ViewModels
{
    public class SaleWithSaleItemViewModel
    {
        public SaleOrderViewModel SaleOrder { get; set; }
        public List<SaleItemViewModel> SaleDetails { get; set; }
        public bool StockSwitch { get; set; }
        public string SaleType { get; set; }
        public SaleWithSaleItemViewModel()
        {
            SaleOrder = new SaleOrderViewModel();
            SaleDetails = new List<SaleItemViewModel>();
            StockSwitch = false;
            SaleType = "Retail";
        }
    }


    public class SaleOrderViewModel
    {
        public string Id { get; set; }
        public DateTime SaleDate { get; set; }
        public string VoucherNo { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal NetTotal { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Decimal Balance { get; set; }
        public Decimal CashAmount { get; set; }
        public string SaleType { get; set; }
        public string UserId { get; set; }

        public SaleOrderViewModel()
        {
            
        }
    }
    
    public class SaleItemViewModel
    {
        public string SaleOrderId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Decimal Total { get; set; }
        public Decimal Amount { get; set; }
        public Decimal DiscountAmount { get; set; }
        public string CategoryId { get; set; }
        public string SaleId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public SaleItemViewModel()
        {
            
        }
    }
}
