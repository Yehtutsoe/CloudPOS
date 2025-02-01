using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CloudPOS.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(CategoryEntity categoryEntity)
        {
            _applicationDbContext.Categories.Add(categoryEntity);
            _applicationDbContext.SaveChanges();

        }

        public void Delete(string Id)
        {
            var entity = _applicationDbContext.Categories.Find(Id);
            if(entity != null)
            {
                _applicationDbContext.Categories.Remove(entity);
                _applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<CategoryEntity> GetById(string Id)
        {
            return _applicationDbContext.Categories.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<CategoryEntity> RetrieveAll()
        {
            return _applicationDbContext.Categories.ToList();
        }

        public void Update(CategoryEntity categoryEntity)
        {
            var existingEntity = _applicationDbContext.Categories.Find(categoryEntity.Id);
            if(existingEntity != null)
            {
                _applicationDbContext.Entry(existingEntity).CurrentValues.SetValues(categoryEntity);
                _applicationDbContext.SaveChanges();
            }

        }
    }
}
