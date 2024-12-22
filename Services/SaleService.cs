using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories;

namespace CloudPOS.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public void Create(SaleViewModel model)
        {
            var entity = new SaleEntity()
            {
                Id = Guid.NewGuid().ToString(),
                SaleDate = model.SaleDate,
                TotalAmount = model.TotalAmount,
                TotalCost = model.TotalCost,
                Profit = model.TotalAmount - model.TotalCost,
                IsActive = true
            };
            _saleRepository.Create(entity);
        }

        public void Delete(string Id)
        {
           var entity= _saleRepository.GetById(Id);
            if (entity != null) {
                _saleRepository.Delete(entity);
            }
        }

        public SaleViewModel GetById(string Id)
        {
            var entity = _saleRepository.GetById(Id);
            
            if( entity != null)
            {
                return new SaleViewModel()
                {
                    
                };
            }
            return null;
        }

        public IEnumerable<SaleViewModel> RetrieveAll()
        {
            var entity = _saleRepository.RetrieveAll();
            return entity.Select(s => new SaleViewModel()
            {
                Id= s.Id,
                SaleDate= s.SaleDate,
                TotalAmount = s.TotalAmount,
                TotalCost = s.TotalCost,
                Profit = s.Profit
            }).ToList();
        }

        public void Update(SaleViewModel model)
        {
            var entity = new SaleEntity()
            {
                Id = model.Id,
                SaleDate = model.SaleDate,
                TotalAmount = model.TotalAmount,
                TotalCost = model.TotalCost,
                Profit = model.Profit
            };
            entity.IsActive = true;
            _saleRepository.Update(entity);
        }
    }
}
