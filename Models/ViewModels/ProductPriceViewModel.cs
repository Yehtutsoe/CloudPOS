﻿namespace CloudPOS.Models.ViewModels
{
    public class ProductPriceViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal WholeSalePrice { get; set; }
        public decimal RetailSalePrice { get; set; }
    }
}
