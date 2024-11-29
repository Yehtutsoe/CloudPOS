using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Supplier")]
    public class SupplierEntity:BaseEntity
    {
        
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(50)]
        public string ContactInformation { get; set; }
        
        [MaxLength(50)]
        public string Address { get; set; }
    }
}
