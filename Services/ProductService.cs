﻿using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;
using Humanizer;


namespace CloudPOS.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(ProductViewModel productViewModel, PriceViewModel priceViewModel)
        {
            ProductEntity productEntity = new ProductEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = productViewModel.Name,
                Code = productViewModel.Code,
                BarCode = productViewModel.BarCode,
                BrandId = productViewModel.BrandId,
                CategoryId = productViewModel.CategoryId,
                CreatedAt = DateTime.Now,
                Description = productViewModel.Description,
                IsActive = true,
            };
           // Console.WriteLine($"Saving product with code: {entity.Code} and name: {entity.Name}");
            _unitOfWork.Products.Create(productEntity);
            PriceEntity priceEntity = new PriceEntity()
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = productEntity.Id,
                IsActive = true,
                CreatedAt = DateTime.Now,
                PricingDate = priceViewModel.PricingDate,
                PurchasePrice = priceViewModel.PurchasePrice,
                RetailSalePrice = priceViewModel.RetailSalePrice,
                WholeSalePrice = priceViewModel.WholeSalePrice
            };
            _unitOfWork.Prices.Create(priceEntity);
            _unitOfWork.Commit();
        }

        public void Delete(string Id)
        {
            try
            {
                ProductEntity entity = _unitOfWork.Products.GetBy(w => w.Id == Id).SingleOrDefault();
                if (entity != null)
                {
                    _unitOfWork.Products.Delete(entity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {

                throw new Exception("Can not delete this product" + e.Message);
            }
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            IEnumerable<ProductViewModel> products = (from p in _unitOfWork.Products.GetAll().ToList()
                                                      join b in _unitOfWork.Brands.GetAll().ToList()
                                                      on p.BrandId equals b.Id
                                                      join c in _unitOfWork.Categories.GetAll().ToList()
                                                      on p.CategoryId equals c.Id
                                                      select new ProductViewModel
                                                      {
                                                          Id = p.Id,
                                                          Name = p.Name,
                                                          Code = p.Code,
                                                          CategoryId = c.Id,
                                                          BrandId = b.Id,
                                                          Description = p.Description,
                                                          CategoryInfo = c.Name,
                                                          BrandInfo = b.Name,
                                                          BarCode = p.BarCode
                                                      });
            return products;
        }

        public ProductViewModel GetById(string Id)
        {
            return _unitOfWork.Products.GetBy(w => w.Id == Id)
                                        .Select(s => new ProductViewModel
                                        {
                                            Id = s.Id,
                                            BrandId = s.BrandId,
                                            CategoryId = s.CategoryId,
                                            Description = s.Description,
                                            Code = s.Code,
                                            Name = s.Name,
                                            BarCode = s.BarCode

                                        }).SingleOrDefault();
        }

        public string GetNextProductCode()
        {
            var lastCode = _unitOfWork.Products.GetNextProductCode();
            if (lastCode != null)
            {
                int newCode = int.Parse(lastCode) + 1;
                return newCode.ToString("D3");
            }
            else
            {
                return "001";
            }
        }

        public IEnumerable<ProductViewModel> GetProductByCategory(string categoryId)
        {
            return _unitOfWork.Products.GetProductByCategory(categoryId);
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            return _unitOfWork.Products.GetProducts();
        }

        public bool IsAlreadyExist(ProductViewModel productViewModel)
        {
            return _unitOfWork.Products.IsAlreadyExist(productViewModel.Code, productViewModel.Name);
        }

        public void Update(ProductViewModel productViewModel)
        {
            var existingProduct = _unitOfWork.Products.GetBy(p => p.Id == productViewModel.Id).FirstOrDefault();
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            existingProduct.Name = productViewModel.Name;
            existingProduct.BrandId = productViewModel.BrandId;
            existingProduct.CategoryId = productViewModel.CategoryId;
            existingProduct.BarCode = productViewModel.BarCode;
            existingProduct.Description = productViewModel.Description;
            _unitOfWork.Products.Update(existingProduct);
            _unitOfWork.Commit();
        }
    }
}