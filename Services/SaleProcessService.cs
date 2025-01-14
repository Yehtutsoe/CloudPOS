using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;
using Humanizer;

namespace CloudPOS.Services
{
    public class SaleProcessService : ISaleProcessService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISaleItemRepository _saleItemRepository;

        public SaleProcessService(ISaleRepository saleRepository,IProductRepository productRepository,ISaleItemRepository saleItemRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _saleItemRepository = saleItemRepository;
        }

        public async Task Create(SaleViewModel model,SaleItemViewModel saleItemViewModel)
        {
            var sale = new SaleEntity
            {
              Id = Guid.NewGuid().ToString(),
              VoucherNo = model.VoucherNo,
              IsActive = true,
              SaleDate = model.SaleDate,
              TotalAmount = model.TotalAmount

            };
            var saleItems = new SaleItemEntity
            {
                SaleId = sale.Id, // foreign key of sale table
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                ProductId =  saleItemViewModel.ProductId, //foregin of product Table
                Quantity = saleItemViewModel.Quantity,
                Remark = saleItemViewModel.Remark            
            };
            await _saleRepository.Create(sale);
            _saleItemRepository.Create(saleItems);
        }

        public void Delete(string Id)
        {
            var sale = _saleRepository.GetById(Id);
            if (sale != null) {
                _saleRepository.Delete(sale);
            }
        }

        public IList<SaleItemViewModel> GetAll()
        {
            var sales = _saleRepository.GetAll();
            var saleItems = _saleItemRepository.RetrieveAll();
            var prodcuts = _productRepository.RetrieveAll();
            var saleItemViewModels = new List<SaleItemViewModel>();
            foreach (var item in saleItems)
            {
                var product = prodcuts.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} not found.");
                }
                var sale = sales.FirstOrDefault(x => x.Id == item.SaleId);
                saleItemViewModels.Add(new SaleItemViewModel
                {
                    ProductInfo = product.Name,
                    SaleDate = sale.SaleDate,
                    UnitPrice = product.SalePrice,
                    Quantity = item.Quantity,
                    Remark = item.Remark,
                    ProductId = item.ProductId
                });
            }
            return saleItemViewModels.OrderByDescending(o => o.ProductId).ToList();
        }

    //    public IList<SaleItemViewModel> GetAll()
    //    {
    //        return _saleItemRepository.RetrieveAll().Select(s => new SaleItemViewModel
    //        {
    //            ProductInfo = _productRepository.GetById(s.Id).Name,
    //            UnitPrice = _productRepository.RetrieveAll().SingleOrDefault().SalePrice,
    //            Quantity = s.Quantity,
    //            Remark = s.Remark,
    //            ProductId = s.ProductId
    //        }).OrderByDescending(o => o.SaleId).ToList();
    //    }
    }
}
