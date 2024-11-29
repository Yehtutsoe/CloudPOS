using CloudPOS.DAO;
using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public class PhoneModelRepository : IPhoneModelRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PhoneModelRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Create(PhoneModelEntity entity)
        {
            _applicationDbContext.PhoneModels.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(string Id)
        {
            var entity = _applicationDbContext.PhoneModels.Find(Id);
            if(entity != null)
            {
                _applicationDbContext.PhoneModels.Remove(entity);
                _applicationDbContext.SaveChanges();
            }
        }

        public IEnumerable<PhoneModelEntity> GetById(string Id)
        {
            return _applicationDbContext.PhoneModels.Where(w => w.Id == Id).ToList();
        }

        public IEnumerable<PhoneModelEntity> RetrieveAll()
        {

            return _applicationDbContext.PhoneModels.ToList();
        }

        public void Update(PhoneModelEntity entity)
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
