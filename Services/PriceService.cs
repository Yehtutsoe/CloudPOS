﻿using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services
{
    public class PriceService : IPriceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PriceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(PriceViewModel priceViewModel)
        {
            PriceEntity priceEntity = new PriceEntity()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                IsActive = true,
                PricingDate = priceViewModel.PricingDate,
                ProductId = priceViewModel.ProductId,
                PurchasePrice = priceViewModel.PurchasePrice,
                WholeSalePrice = priceViewModel.WholeSalePrice,
                RetailSalePrice = priceViewModel.RetailSalePrice
            };
            _unitOfWork.Prices.Create(priceEntity);
            _unitOfWork.Commit();
        }

        public void Delete(string Id)
        {
            var existingEntity = _unitOfWork.Prices.GetBy(p =>p.Id == Id).FirstOrDefault();
            if (existingEntity != null)
            {
                _unitOfWork.Prices.Delete(existingEntity);
                _unitOfWork.Commit();
            }
            else
            {
                throw new Exception("Price not found to delete");
            }
        }

        public IEnumerable<PriceViewModel> GetAll()
        {
            var prices = (from price in _unitOfWork.Prices.GetAll()
                          join product in _unitOfWork.Products.GetAll()
                          on price.Id equals product.Id
                          select new PriceViewModel
                          {
                              Id = price.Id,
                              ProductId = product.Id,
                              ProductName = product.Name,
                              PricingDate = price.PricingDate,
                              PurchasePrice = price.PurchasePrice,
                              RetailSalePrice = price.RetailSalePrice,
                              WholeSalePrice = price.WholeSalePrice
                          }).ToList();
            return prices;
        }

        public PriceViewModel GetById(string Id)
        {
            return _unitOfWork.Prices.GetBy(p => p.Id == Id).Select(s => new PriceViewModel
            {

                Id = s.Id,
                PricingDate = s.PricingDate,
                ProductId= s.ProductId,
                WholeSalePrice = s.WholeSalePrice,
                RetailSalePrice = s.RetailSalePrice,
                PurchasePrice  = s.PurchasePrice,
                
            }).FirstOrDefault();
        }

        public IEnumerable<PriceViewModel> GetPrices()
        {
            return _unitOfWork.Prices.GetPrices();
        }

        public ProductPriceViewModel ProductDetailsByBarCode(string BarCode)
        {
            return _unitOfWork.Prices.GetProductDetailsByBarCode(BarCode);
        }

        public void Update(PriceViewModel priceViewModel)
        {
            PriceEntity entity = new PriceEntity()
            {
                Id = priceViewModel.Id,
                IsActive = true,
                PricingDate = priceViewModel.PricingDate,
                ProductId = priceViewModel.ProductId,
                PurchasePrice = priceViewModel.PurchasePrice,
                WholeSalePrice = priceViewModel.WholeSalePrice,
                RetailSalePrice = priceViewModel.RetailSalePrice  
            };
            _unitOfWork.Prices.Update(entity);
            _unitOfWork.Commit();
        }
    }
}
