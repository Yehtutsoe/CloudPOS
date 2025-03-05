using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class PriceRepository : BaseRepository<PriceEntity>, IPriceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PriceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<PriceViewModel> GetPrices()
        {
            return _dbContext.Prices.Select(s => new PriceViewModel
            {
                Id = s.Id
            }).ToList();
        }

        public ProductPriceViewModel GetProductDetailsByBarCode(string barCode)
        {
            var productDetails = (from pr in _dbContext.Prices
                                  join po in _dbContext.Products on pr.Id equals po.Id
                                  where po.BarCode == barCode
                                  select new ProductPriceViewModel
                                  {
                                      ProductName = po.Name,
                                      RetailSalePrice = pr.RetailSalePrice,
                                      WholeSalePrice = pr.WholeSalePrice
                                  }).FirstOrDefault();
            return productDetails;
        }
    }
}
