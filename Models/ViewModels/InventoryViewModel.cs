namespace CloudPOS.Models.ViewModels
{
    public class InventoryViewModel
    {
        public string Id { get; set; }
        public string ProductInfo { get; set; }
        public DateTime CreateAt { get; set; }
        public int Quantity { get; set; }
    }
}
