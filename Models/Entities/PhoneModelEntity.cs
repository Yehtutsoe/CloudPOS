using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Model")]
    public class PhoneModelEntity:BaseEntity
    {
      
        [MaxLength(20)]
        public string Name { get; set; }
       
        [MaxLength(25)]
        public string Brand { get; set; }
        
        [MaxLength(50)]
        public string Specification { get; set; }
        [MaxLength(15)]
        public DateTime? ReleaseDate { get; set; }
    }
}
