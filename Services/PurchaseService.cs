using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPurchaseItemRepository _purchaseItemRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository, IPurchaseItemRepository purchaseItemRepository)
        {
            _purchaseRepository = purchaseRepository;
            _purchaseItemRepository = purchaseItemRepository;
        }
        public void Create(PurchaseViewModel purchaseViewModel, PurchaseItemViewModel purchaseItemViewModel)
        {
            var purchaseEntity = new PurchaseEntity()
            {
                Id = Guid.NewGuid().ToString(),
                DeliveryStatus = purchaseViewModel.DeliveryStatus,
                PurchaseDate = purchaseViewModel.PurchaseDate,
                IsActive = true,
                TotalCost = purchaseViewModel.TotalCost,
                SupplierId = purchaseViewModel.SupplierId
            };
            var purchaseItemEntity = new PurchaseItemEntity()
            {
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                PurchaseId = purchaseEntity.Id,
                ProductId = purchaseItemViewModel.ProductId,
                CostPerUnit = purchaseItemViewModel.CostPerUnit,
                Quantity = purchaseItemViewModel.Quantity
            };
            _purchaseRepository.Create(purchaseEntity);
            _purchaseItemRepository.Create(purchaseItemEntity);
        }

        public void Delete(string Id)
        {
            _purchaseRepository.Delete(Id);
        }

        public PurchaseViewModel GetActiveSupplier()
        {
            IList<SupplierViewModel> suppliersView = _purchaseRepository.GetActiveSupplier()
                                                                    .Select(s => new SupplierViewModel()
                                                                    {
                                                                        Id = s.Id,
                                                                        Name = s.Name
                                                                    }).ToList();
            return new PurchaseViewModel()
            {
                SupplierViewModels = suppliersView
            };
        }

        public PurchaseViewModel GetById(string Id)
        {
            var entity = _purchaseRepository.GetById(Id).SingleOrDefault();
            if (entity == null)
            {
                return null;
            }
            return new PurchaseViewModel()
            {
                Id = entity.Id,
                PurchaseDate = entity.PurchaseDate,
                DeliveryStatus = entity.DeliveryStatus,
                TotalCost = entity.TotalCost,
                SupplierId = entity.SupplierId
            };
        }

        public IEnumerable<PurchaseViewModel> RetrieveAll()
        {
            var enitity = _purchaseRepository.RetrieveAll();
            return enitity.Select(s => new PurchaseViewModel()
            {
                Id = s.Id,
                PurchaseDate = s.PurchaseDate,
                DeliveryStatus = s.DeliveryStatus,
                TotalCost = s.TotalCost,
                SupplierId = s.SupplierId,
                SupplierInfo = s.Suppliers.Name
            }).ToList();
        }

        public void Update(PurchaseViewModel purchaseViewModel)
        {
            var entity = new PurchaseEntity()
            {
                Id = purchaseViewModel.Id,
                PurchaseDate = purchaseViewModel.PurchaseDate,
                DeliveryStatus = purchaseViewModel.DeliveryStatus,
                TotalCost = purchaseViewModel.TotalCost,
                SupplierId = purchaseViewModel.SupplierId,
            };
            entity.IsActive = true;
            _purchaseRepository.Update(entity);
        }
    }
}
