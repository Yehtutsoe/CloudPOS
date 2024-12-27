using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudPOS.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SaleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
