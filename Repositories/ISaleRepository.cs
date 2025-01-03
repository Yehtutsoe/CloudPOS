﻿using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface ISaleRepository
    {
         Task<IEnumerable<SaleEntity>> GetAllSaleAsync();
        Task<SaleEntity> GetById(string Id);
        Task Create(SaleEntity entity,SaleItemEntity saleItemEntity);
        Task Update(SaleEntity entity);
        Task Delete(string Id);
    }
}
