﻿using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;

namespace CloudPOS.Repositories
{
    public interface IProductRepository
    {
        void Create(ProductViewModel productViewModel);
        IList<ProductViewModel> RetrieveAll();
        ProductViewModel GetById(string Id);
        void Delete(string Id);
        IList<CategoryViewModel> GetCategories();
        IList<BrandViewModel> GetPhonesModels();
        void Update(ProductEntity productEntity);

    }
}
