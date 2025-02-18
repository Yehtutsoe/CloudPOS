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

        public void CreateSale(SaleViewModel saleViewModel, List<SaleItemViewModel> saleItemsViewModel)
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
                var product = _unitOfWork.Products.GetBy(w => w.Id == itemViewModel.ProductId)
                                                   .Select(s =>new ProductEntity()
                                                   {
                                                       Id = s.Id,
                                                       Name = s.Name,
                                                       Quantity = s.Quantity
                                                   }).SingleOrDefault();
                if (product == null)
                {
                    throw new Exception($"Product with ID {itemViewModel.ProductId} not found.");
                }
                if (product.Quantity < itemViewModel.Quantity)
                {
                    throw new Exception($"Not enough stock for {product.Name}. Available: {product.Quantity}, Requested: {itemViewModel.Quantity}");
                }

                product.Quantity -= itemViewModel.Quantity;
                _unitOfWork.Products.Update(product);

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

             _unitOfWork.Sales.Create(sale);
             _unitOfWork.SaleItems.Create(saleItems);

            _unitOfWork.Commit();
        }

        public void DeleteSale(string saleId)
        {
            var sale = _unitOfWork.Sales.GetById(saleId);
            if (sale != null)
            {
                _unitOfWork.Sales.Delete(sale);
                _unitOfWork.Commit();
            }
        }

        public IList<SaleItemViewModel> GetAllSales()
        {
            var sales = _unitOfWork.Sales.GetAll();
            var saleItems = _unitOfWork.SaleItems.RetrieveAll();
            var products = _unitOfWork.Products.GetAll();

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
