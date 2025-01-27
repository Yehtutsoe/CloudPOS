using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class StockIncomeService : IStockIncomeService
    {
        private readonly IStockIncomeRepository _stockIncomeRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public StockIncomeService(IStockIncomeRepository purchaseRepository,IInventoryRepository inventoryRepository)
        {
            _stockIncomeRepository = purchaseRepository;
            _inventoryRepository = inventoryRepository;
        }
        public bool CheckProductAlreadyExistInInventory(string productId)
        {
           return _inventoryRepository.GetById(productId).Any();
        }
        public void Create(StockIncomeViewModel stockViewModel)
        {
            var stockIncomeEntity = new StockIncomeEntity()
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
            
            _stockIncomeRepository.Create(stockIncomeEntity);
            if (CheckProductAlreadyExistInInventory(stockViewModel.ProductId)) { 
                UpdateInventory(stockIncomeEntity);
            }
            else
            {
                BalanceInventory(stockIncomeEntity);
            }

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
                                                                            Name = s.Name 
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
                SupplierId = entity.SupplierId,
                ProductId = entity.ProductId,
                ProductViewModels = _stockIncomeRepository.GetActiveProducts().Select(s => new ProductViewModel() { Id = s.Id,Name=s.Name}).ToList(),
                SupplierViewModels = _stockIncomeRepository.GetActiveSupplier().Select(s => new SupplierViewModel() { Id = s.Id,Name =s.Name}).ToList()
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
                ProductId = purchaseViewModel.ProductId,
                CreatedAt = DateTime.Now,
                
            };
            entity.IsActive = true;
            _stockIncomeRepository.Update(entity);
        }
        private void BalanceInventory(StockIncomeEntity entity)
        {
            var inventoryEntity = new InventoryEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AdjustmentDate = DateTime.Now,
                IsActive = true,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity
            };
            _inventoryRepository.Create(inventoryEntity);
        }

        private void UpdateInventory(StockIncomeEntity stockIncomeEntity)
        {
            var inventoryEntity = _inventoryRepository.GetById(stockIncomeEntity.Id).FirstOrDefault();
            inventoryEntity.Quantity += stockIncomeEntity.Quantity;
            _inventoryRepository.Update(inventoryEntity);
        }
    }
}
