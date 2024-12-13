using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public void Create(PurchaseViewModel purchaseViewModel)
        {
            var entity = new PurchaseEntity()
            {
                Id = Guid.NewGuid().ToString(),
                DeliveryStatus = purchaseViewModel.DeliveryStatus,
                PurchaseDate = purchaseViewModel.PurchaseDate,
                TotalCost = purchaseViewModel.TotalCost,
                SupplierId = purchaseViewModel.SupplierId,
            };
            _purchaseRepository.Create(entity);
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public PurchaseViewModel GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PurchaseViewModel> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(PurchaseViewModel purchaseViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
