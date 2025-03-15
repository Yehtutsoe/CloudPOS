using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Sale")]
    public class SaleEntity:BaseEntity
    {
       
        
        public DateTime SaleDate { get; set; }
        public string VoucherNo { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal NetTotal { get; set; }
        public Decimal CashAmount { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public Decimal? DiscountPercent { get; set; }
        public Decimal TotalReturnAmount { get; set; }
        public Decimal Balance { get; set; }
        public string SaleType { get; set; }
        public string UserId { get; set; }

    }
}
