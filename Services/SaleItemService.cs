using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly ISaleItemRepository _saleItemRepository;

        public SaleItemService(ISaleItemRepository saleItemRepository)
        {
            _saleItemRepository = saleItemRepository;
        }
        public void Create(SaleItemViewModel saleItemViewModel)
        {
            var entity = new SaleItemEntity()
            {
            
            };
            _saleItemRepository.Create(entity);
        }
    }
}
