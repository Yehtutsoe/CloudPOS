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
    }
}
