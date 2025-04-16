using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Product")]
    public class ProductEntity:BaseEntity
    {  
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string BarCode { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }

    }
 
}
