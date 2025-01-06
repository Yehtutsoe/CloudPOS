using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class SaleProcessViewModel
    {
        public string Id { get; set; }
        public DateTime SaleDate { get; set; }
        public Decimal UnitPrice { get; set; }
    }
}
