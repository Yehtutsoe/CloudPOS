using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class InventoryViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Transaction Type is required.")]
        public string TransactionType { get; set; }
        public DateTime EarliestDate { get; set; }
    }
}
