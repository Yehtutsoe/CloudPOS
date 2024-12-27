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
    }
}
