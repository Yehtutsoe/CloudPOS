namespace CloudPOS.Models.Entities
{
    public class ProductImageEntity:BaseEntity
    {
        public string ProductId { get; set; }
        public Byte[] ImageData { get; set; }
        public string ImageName { get; set; }
    }
}
