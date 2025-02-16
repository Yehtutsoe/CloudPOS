﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPOS.Models.Entities
{
    [Table("SaleItem")]
    public class SaleItemEntity:BaseEntity
    {
        
        [MaxLength(20)]
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }
        public string SaleId { get; set; }
        [ForeignKey(nameof(SaleId))]
        public SaleEntity Sales { get; set; }
        public string Remark { get; set; }
    }
}
