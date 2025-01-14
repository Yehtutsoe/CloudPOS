using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;
using Humanizer;

namespace CloudPOS.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly ISaleItemRepository _saleItemRepository;

        public SaleItemService(ISaleItemRepository saleItemRepository)
        {
            _saleItemRepository = saleItemRepository;
        }

        public SaleItemViewModel GetActiveProduct()
        {
            IList<ProductViewModel> products = _saleItemRepository.GetProducts().Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList();
            return new SaleItemViewModel()
            {
               ProductViewModels = products
            };
        }
    }
}
