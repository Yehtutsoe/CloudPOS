﻿using CloudPOS.DAO;
using CloudPOS.Models.Entities;
using CloudPOS.Repositories.Common;

namespace CloudPOS.Repositories.Domain
{
    public class SaleItemRepository : BaseRepository<SaleItemEntity>, ISaleItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
