using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<CategoryViewModel> GetCategorys()
        {
            return _dbContext.Categories.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
            
        }

        public string GetLastCategoryCode()
        {
            var lastCode = _dbContext.Categories.OrderByDescending(c => c.Code).FirstOrDefault();
            return lastCode != null ? lastCode.Code : null;
        }

        public bool IsAlreadyExist(string Code, string Name)
        {
            return _dbContext.Categories.Where(w => w.Code != Code && w.Name == Name).Any();
        }
    }
}
