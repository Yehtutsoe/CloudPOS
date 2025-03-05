﻿using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public Decimal SalePrice { get; set; }
        public Decimal DiscountPrice { get; set; }
        public int Quantity { get; set; }
        public Decimal CostPrice { get; set; }
        public string BrandId { get; set; }
        public string BrandInfo { get; set; }
        public IList<BrandViewModel> BrandViewModels { get; set; } //List for UI Binding
        public string CategoryId { get; set; }
        public string CategoryInfo { get; set; }
        public IList<CategoryViewModel> CategoryViewModels { get; set; } // List for UI Binding
        
    }
}
