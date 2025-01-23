using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }
}
