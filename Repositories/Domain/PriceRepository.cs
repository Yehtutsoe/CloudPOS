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
            var details = (from p in _dbContext.Prices
                           join pr in _dbContext.Products
                           on p.ProductId equals pr.Id
                           select new ProductPriceViewModel
                           {
                               ProductId = p.ProductId,
                               RetailSalePrice = p.RetailSalePrice,
                               WholeSalePrice = p.WholeSalePrice
                           }).FirstOrDefault();

            return details;
        }
    }
}
