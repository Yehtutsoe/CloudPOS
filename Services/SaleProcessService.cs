using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services
{
    public class SaleService : ISaleProcessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateSale(SaleViewModel saleViewModel, List<SaleItemViewModel> saleItemsViewModel)
        {
            var sale = new SaleEntity
            {
                Id = Guid.NewGuid().ToString(),
                VoucherNo = saleViewModel.VoucherNo,
                IsActive = true,
                SaleDate = saleViewModel.SaleDate,
                TotalAmount = saleViewModel.TotalAmount,
                UserId = saleViewModel.UserId
            };

            var saleItems = new List<SaleItemEntity>();

            foreach (var itemViewModel in saleItemsViewModel)
            {
                var product = _unitOfWork.ProductRepository.GetById(itemViewModel.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {itemViewModel.ProductId} not found.");
                }
                if (product.Quantity < itemViewModel.Quantity)
                {
                    throw new Exception($"Not enough stock for {product.Name}. Available: {product.Quantity}, Requested: {itemViewModel.Quantity}");
                }

                product.Quantity -= itemViewModel.Quantity;
                _unitOfWork.ProductRepository.Update(product);

                saleItems.Add(new SaleItemEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    SaleId = sale.Id,
                    ProductId = itemViewModel.ProductId,
                    Quantity = itemViewModel.Quantity,
                    Remark = itemViewModel.Remark,
                    IsActive = true
                });
            }

            await _unitOfWork.SaleRepository.Create(sale);
            await _unitOfWork.SaleItemRepository.CreateRange(saleItems);

            await _unitOfWork.Commit();
        }

        public void DeleteSale(string saleId)
        {
            var sale = _unitOfWork.SaleRepository.GetById(saleId);
            if (sale != null)
            {
                _unitOfWork.SaleRepository.Delete(sale);
                _unitOfWork.Commit();
            }
        }

        public IList<SaleItemViewModel> GetAllSales()
        {
            var sales = _unitOfWork.SaleRepository.GetAll();
            var saleItems = _unitOfWork.SaleItemRepository.RetrieveAll();
            var products = _unitOfWork.ProductRepository.RetrieveAll();

            var saleItemViewModels = saleItems.Select(item =>
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                var sale = sales.FirstOrDefault(s => s.Id == item.SaleId);

                if (product == null || sale == null) return null;

                return new SaleItemViewModel
                {
                    ProductInfo = product.Name,
                    SaleDate = sale.SaleDate,
                    UnitPrice = product.SalePrice,
                    Quantity = item.Quantity,
                    Remark = item.Remark,
                    ProductId = item.ProductId
                };
            }).Where(x => x != null).ToList();

            return saleItemViewModels.OrderByDescending(o => o.SaleDate).ToList();
        }
    }
}
