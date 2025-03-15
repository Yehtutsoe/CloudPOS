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
            return _dbContext.Prices
            .Where(p => p.Products.BarCode == barCode && p.IsActive)
            .Select(p => new ProductPriceViewModel
            {
                ProductId = p.ProductId,
                RetailSalePrice = p.RetailSalePrice,
                WholeSalePrice = p.WholeSalePrice,
                // Add other fields as necessary
            })
            .FirstOrDefault();
        }
    }
}
