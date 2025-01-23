using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class StockIncomeService : IStockIncomeService
    {
        private readonly IStockIncomeRepository _stockIncomeRepository;

        public StockIncomeService(IStockIncomeRepository purchaseRepository)
        {
            _stockIncomeRepository = purchaseRepository;
           
        }
        public void Create(StockIncomeViewModel stockViewModel)
        {
            var purchaseEntity = new StockIncomeEntity()
            {
                Id = Guid.NewGuid().ToString(),
                DeliveryStatus = stockViewModel.DeliveryStatus,
                PurchaseDate = stockViewModel.PurchaseDate,
                IsActive = true,
                Quantity = stockViewModel.Quantity,
                SupplierId = stockViewModel.SupplierId,
                ProductId = stockViewModel.ProductId,
                CreatedAt= DateTime.Now
            };
            
            _stockIncomeRepository.Create(purchaseEntity);
        }

        public void Delete(string Id)
        {
            _stockIncomeRepository.Delete(Id);
        }

        public StockIncomeViewModel GetActiveSuppliersAndProducts()
        {
            IList<SupplierViewModel> suppliers = _stockIncomeRepository.GetActiveSupplier()
                                                                   .Select(s => new SupplierViewModel()
                                                                   {
                                                                       Id = s.Id,
                                                                       Name = s.Name
                                                                   }).ToList();
            IList<ProductViewModel> products = _stockIncomeRepository.GetActiveProducts()
                                                                      .Select(s => new ProductViewModel()
                                                                        {
                                                                            Id = s.Id,
                                                                            Name = s.Name,
                                                                        }).ToList();
            return new StockIncomeViewModel()
            {
                SupplierViewModels = suppliers,
                ProductViewModels = products
            };
        }

        public StockIncomeViewModel GetById(string Id)
        {
            var entity = _stockIncomeRepository.GetById(Id).SingleOrDefault();
            if (entity == null)
            {
                return null;
            }
            return new StockIncomeViewModel()
            {
                Id = entity.Id,
                PurchaseDate = entity.PurchaseDate,
                DeliveryStatus = entity.DeliveryStatus,
                Quantity = entity.Quantity,
                SupplierId = entity.SupplierId
            };
        }

        public IEnumerable<StockIncomeViewModel> RetrieveAll()
        {
            var enitity = _stockIncomeRepository.RetrieveAll();
            return enitity.Select(s => new StockIncomeViewModel()
            {
                Id = s.Id,
                PurchaseDate = s.PurchaseDate,
                DeliveryStatus = s.DeliveryStatus,
                Quantity = s.Quantity,
                SupplierId = s.SupplierId,
                SupplierInfo = s.Suppliers.Name,
                ProductId = s.ProductId,
                ProductInfo = s.Products.Name
            }).ToList();
        }

        public void Update(StockIncomeViewModel purchaseViewModel)
        {
            var entity = new StockIncomeEntity()
            {
                Id = purchaseViewModel.Id,
                PurchaseDate = purchaseViewModel.PurchaseDate,
                DeliveryStatus = purchaseViewModel.DeliveryStatus,
                Quantity = purchaseViewModel.Quantity,
                SupplierId = purchaseViewModel.SupplierId,
            };
            entity.IsActive = true;
            _stockIncomeRepository.Update(entity);
        }
    }
}
