﻿using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;

        public SaleController(ISaleService saleService,IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }
        public async Task<IActionResult> Entry()
        {
            var saleViewModel = new SaleProcessViewModel()
            {
                ProductViewModels = GetProductList()
            };   
            return View(saleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Entry(SaleProcessViewModel saleViewModel)
        {
            await  _saleService.Create(saleViewModel);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> List()
        {
            var sales = await _saleService.GetAllSale();
            var saleViewModel = sales.Select(s => new SaleProcessViewModel
            {
                Id = s.Id,
                SaleDate = s.SaleDate,
                UnitPrice = s.SaleItems?.Sum(item => item.TotalPrice / item.Quantity) ?? 0,
                Quantity = s.SaleItems?.Sum(item => item.Quantity) ?? 0,
                ProductId = string.Join(", ", s.SaleItems.Select(item => item.ProductId) ?? new List<string>()),
                ProdcutInfo =string.Join(", ", s.SaleItems.Select(item => item.Products?.Name) ?? new List<string>())

            }).ToList();
            return View(saleViewModel);
        }
        public IActionResult Edit(string Id)
        {
            return View(_saleService.GetById(Id));
        }
        public IActionResult Delete(string Id) {
            _saleService.Delete(Id);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Update(SaleProcessViewModel saleViewModel) {
            _saleService.Update(saleViewModel);
            return RedirectToAction("List");
        }

        private IList<ProductViewModel> GetProductList()
        {
            // Example: Fetch products from the database or repository
            return _productService.RetrieveAll().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name=p.Name + p.CategoryInfo
            }).ToList();
        }
    }
}
