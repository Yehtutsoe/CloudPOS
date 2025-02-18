using CloudPOS.DAO;
using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BrandRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Delete(string Id)
        {
            var entity = _applicationDbContext.PhoneModels.Find(Id);
            if(entity != null)
            {
                _applicationDbContext.PhoneModels.Remove(entity);
                _applicationDbContext.SaveChanges();
            }

        public IEnumerable<BrandEntity> GetById(string Id)
        {
            return _applicationDbContext.PhoneModels.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<BrandEntity> RetrieveAll()
        {

            return _applicationDbContext.PhoneModels.ToList();
        }

        public void Update(BrandEntity entity)
        {
            var existingEntity = _applicationDbContext.PhoneModels.Find(entity.Id);
            if(existingEntity != null)
            {
                _applicationDbContext.PhoneModels.Entry(existingEntity).CurrentValues.SetValues(entity);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
