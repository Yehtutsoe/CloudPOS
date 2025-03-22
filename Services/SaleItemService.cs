using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services
{
    public class SaleItemService : ISaleItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(SaleItemViewModel saleItemViewModel)
        {
            SaleItemEntity entity = new SaleItemEntity()
            {
                Id = saleItemViewModel.SaleOrderId,
                SaleId = saleItemViewModel.SaleId,
                CreatedAt = DateTime.Now,
                IsActive  = true,
                ProductId = saleItemViewModel.ProductId,
                Price = saleItemViewModel.Price,
                DiscountAmount = saleItemViewModel.DiscountAmount,
                Quantity = saleItemViewModel.Quantity,
                TotalPrice = saleItemViewModel.Total,
                SaleAmount = saleItemViewModel.Amount,
                
            };
            _unitOfWork.SaleItems.Create(entity);
        }
    }
}
