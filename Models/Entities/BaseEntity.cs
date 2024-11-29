using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }
}
