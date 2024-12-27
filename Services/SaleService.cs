using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task Create(SaleProcessViewModel model)
        {
            var sale = new SaleEntity
            {
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                TotalAmount = model.ProductViewModels.Sum(p => p.SalePrice * (decimal)p.Quantity),
                SaleItems = model.ProductViewModels.Select(p => new SaleItemEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = p.Id,
                    Quantity = model.Quantity,
                    TotalPrice = p.SalePrice * (decimal)p.Quantity,
                }).ToList()
            };
            await _saleRepository.Create(sale);
        }

        public async Task Delete(string Id)
        {
           await _saleRepository.Delete(Id);
        }

        public async Task<IEnumerable<SaleEntity>> GetAllSale()
        {
            return await _saleRepository.GetAllSaleAsync();
        }

        public async Task<SaleEntity> GetById(string id)
        {
            return await _saleRepository.GetById(id);
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
