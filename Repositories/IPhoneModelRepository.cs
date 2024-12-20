﻿using CloudPOS.Models.Entities;

namespace CloudPOS.Repositories
{
    public interface IPhoneModelRepository
    {
        void Create(PhoneModelEntity entity);
        IEnumerable<PhoneModelEntity> GetById(string Id);
        IEnumerable<PhoneModelEntity> RetrieveAll();
        void Delete(string Id);
        void Update(PhoneModelEntity entity);
    }
}
