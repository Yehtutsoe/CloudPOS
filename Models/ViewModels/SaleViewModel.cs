using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class SaleViewModel
    {
        public string Id { get; set; }
        public string VoucherNo { get; set; }
        public DateTime SaleDate { get; set; }
        public Decimal TotalAmount { get; set; }
    }
}
