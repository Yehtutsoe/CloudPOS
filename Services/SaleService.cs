using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;

        public SaleService(ISaleRepository saleRepository,IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public async Task Create(SaleProcessViewModel model)
        {
            var sale = new SaleEntity
            {
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                TotalAmount = _productRepository.GetById(model.ProductId).SalePrice * model.Quantity

            };
            var saleItems = new SaleItemEntity
            {
                Id = Guid.NewGuid().ToString(),
                SaleId = sale.Id,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                TotalPrice = _productRepository.GetById(model.ProductId).SalePrice * model.Quantity

            };
            await _saleRepository.Create(sale,saleItems);
         

        }

        public async Task Delete(string Id)
        {
           await _saleRepository.Delete(Id);
        }

        public async Task<IEnumerable<SaleProcessViewModel>> GetAllSale()
        {
            var entities = await _saleRepository.GetAllSaleAsync();
            return entities.Select(s => new SaleProcessViewModel
            {
                Id = s.Id,
                SaleDate = s.SaleDate,
                UnitPrice = s.SaleItems != null && s.SaleItems.Any()
                    ? s.SaleItems.Average(item => item.TotalPrice / (item.Quantity > 0 ? item.Quantity : 1))
                    : 0,
                Quantity = s.SaleItems?.Sum(item => item.Quantity) ?? 0,
                ProductId = string.Join(", ", s.SaleItems.Select(item => item.ProductId) ?? new List<string>()),
                ProductInfo = string.Join(", ", s.SaleItems.Select(item => item.Products?.Name ?? "Unknown") ?? new List<string>())
            }).ToList();
        }
    

        public async Task<SaleProcessViewModel> GetById(string id)
        {
            var entity =  await _saleRepository.GetById(id);
            if(entity == null)
            {
                return null;
            }
            return new SaleProcessViewModel()
            {
                Id = entity.Id,
                Quantity = entity.SaleItems.First().Quantity,
                SaleDate = entity.SaleDate,
                ProductId = entity.SaleItems.First().ProductId,
                ProductInfo = entity.SaleItems.First().Products.Name
            };
        }

        public async Task Update(SaleProcessViewModel model)
        {
            var sale =await _saleRepository.GetById(model.Id);
            if(sale  != null)
            {
                sale.SaleDate = model.SaleDate;
                sale.TotalAmount = model.UnitPrice * model.Quantity;
                sale.SaleItems.First().Quantity = model.Quantity;
                sale.SaleItems.First().TotalPrice = model.UnitPrice * model.Quantity;
                await _saleRepository.Update(sale);
            }

        }
    }
}
