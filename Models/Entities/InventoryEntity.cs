using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Inventory")]
    public class InventoryEntity : BaseEntity
    {
        public int Quantity { get; set; } 
        public DateTime EarliestDate { get; set; }
        public string ProductId { get; set; }

        public string CategoryId { get; set; }

    }
}
