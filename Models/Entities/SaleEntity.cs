using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Sale")]
    public class SaleEntity:BaseEntity
    {
       
        [MaxLength(50)]
        public DateTime SaleDate { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal TotalCost { get; set; }
        public Decimal Profit { get; set; }
        public IList<SaleItemEntity> SaleItems { get; set; }

    }
}
