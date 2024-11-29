using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Category")]
    public class CategoryEntity:BaseEntity
    {
   
        [MaxLength(30)]
        public string Name { get; set; }
       
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
