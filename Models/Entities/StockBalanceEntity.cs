using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("StockBalance")]
    public class StockBalanceEntity:BaseEntity
    {
        public DateTime Date { get; set; }
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Prodcuts { get; set; }
        public string CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Categorys { get; set; }
        public int Quantity { get; set; }
    }
}
