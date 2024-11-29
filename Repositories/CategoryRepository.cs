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
            _applicationDbContext.Categorys.Add(categoryEntity);
            _applicationDbContext.SaveChanges();

        }

        public void Delete(string Id)
        {
            var entity = _applicationDbContext.Categorys.Find(Id);
            if(entity != null)
            {
                _applicationDbContext.Categorys.Remove(entity);
                _applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<CategoryEntity> GetById(string Id)
        {
            return _applicationDbContext.Categorys.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<CategoryEntity> RetrieveAll()
        {
            return _applicationDbContext.Categorys.ToList();
        }

        public void Update(CategoryEntity categoryEntity)
        {
            var existingEntity = _applicationDbContext.Categorys.Find(categoryEntity.Id);
            if(existingEntity != null)
            {
                _applicationDbContext.Entry(existingEntity).CurrentValues.SetValues(categoryEntity);
                _applicationDbContext.SaveChanges();
            }

        }
    }
}
