using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("Brand")]
    public class BrandEntity:BaseEntity
    {
        public string Code { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }

        public string CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual CategoryEntity Categorys { get; set; }
    }
}
