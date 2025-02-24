using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class BrandRepository : BaseRepository<BrandEntity>, IBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<BrandViewModel> GetBrandByCategory(string CategoryId)
        {
            return _dbContext.Brands.Where(w => w.CategoryId == CategoryId)
                                    .Select(s => new BrandViewModel
                                    {
                                        Id = s.Id,
                                        Name = s.Name
                                    }).ToList();
        }

        public IEnumerable<BrandViewModel> GetBrands()
        {
            return _dbContext.Brands.Select(s => new BrandViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public string GetLastBrandCode()
        {
            var lastBrand = _dbContext.Brands.OrderByDescending(s => s.Code).FirstOrDefault();
            return lastBrand != null ? lastBrand.Code : null;
        }

        public bool IsAlreadyExist(string Code, string Name)
        {
            return _dbContext.Brands.Where(w => w.Name == Name && w.Code != Code).Any();
        }
    }
}
