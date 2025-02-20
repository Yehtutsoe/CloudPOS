using CloudPOS.Models.Entities;
using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;


namespace CloudPOS.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(ProductViewModel productViewModel)
        {
            var entity = new ProductEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = productViewModel.Name,
                ProductCode = productViewModel.ProductCode,
                CostPrice = productViewModel.CostPrice,
                DiscountPrice = productViewModel.DiscountPrice,
                SalePrice = productViewModel.SalePrice,
                BrandId = productViewModel.BrandId,
                CategoryId = productViewModel.CategoryId,
                CreatedAt = DateTime.Now,
                IsActive = true,
                Quantity = productViewModel.Quantity
            };
            _unitOfWork.Products.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string Id)
        {
            try
            {
                ProductEntity entity = _unitOfWork.Products.GetBy(w => w.Id == Id).SingleOrDefault();
                if (entity != null)
                {
                    _unitOfWork.Products.Delete(entity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {

                throw new Exception("Can not delete this product" + e.Message);
            }
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            IEnumerable<ProductViewModel> products = (from p in _unitOfWork.Products.GetAll()
                                                      join b in _unitOfWork.Brands.GetAll()
                                                      on p.BrandId equals b.Id
                                                      join c in _unitOfWork.Categories.GetAll()
                                                      on p.CategoryId equals c.Id
                                                      select new ProductViewModel
                                                      {
                                                          Id = p.Id,
                                                          Name = p.Name,
                                                          ProductCode = p.ProductCode,
                                                          CategoryId = c.Id,
                                                          BrandId = b.Id,
                                                          Quantity = p.Quantity,
                                                          DiscountPrice = p.DiscountPrice,
                                                          CostPrice = p.CostPrice,
                                                          SalePrice = p.SalePrice,
                                                          Description = p.Description,
                                                          CategoryInfo = c.Name,
                                                          BrandInfo = b.Name
                                                      });
            return products;
        }

        public ProductViewModel GetById(string Id)
        {
            return _unitOfWork.Products.GetBy(w => w.Id == Id)
                                        .Select(s => new ProductViewModel
                                        {
                                            Id = s.Id,
                                            BrandId = s.BrandId,
                                            CategoryId = s.CategoryId,
                                            Description = s.Description,
                                            ProductCode = s.ProductCode,
                                            Name = s.Name,
                                            Quantity = s.Quantity,
                                            CostPrice = s.CostPrice,
                                            SalePrice = s.SalePrice,
                                            DiscountPrice = s.DiscountPrice

                                        }).FirstOrDefault();
        }

        public string GetNextProductCode()
        {
            var lastCode = _unitOfWork.Products.GetNextProductCode();
            if (lastCode != null)
            {
                int newCode = int.Parse(lastCode) + 1;
                return newCode.ToString("D4");
            }
            else
            {
                return "P001";
            }
        }

        public IEnumerable<ProductViewModel> GetProductByCategory(string categoryId)
        {
            return _unitOfWork.Products.GetProductByCategory(categoryId);
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            return _unitOfWork.Products.GetProducts();
        }

        public bool IsAlreadyExist(ProductViewModel productViewModel)
        {
            return _unitOfWork.Products.IsAlreadyExist(productViewModel.ProductCode, productViewModel.Name);
        }

        public void Update(ProductViewModel productViewModel)
        {
            var existingProduct = _unitOfWork.Products.GetBy(p => p.Id == productViewModel.Id).FirstOrDefault();
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            existingProduct.Id = productViewModel.Id;
            existingProduct.Name = productViewModel.Name;
            existingProduct.ProductCode = productViewModel.ProductCode;
            existingProduct.CostPrice = productViewModel.CostPrice;
            existingProduct.SalePrice = productViewModel.SalePrice;
            existingProduct.DiscountPrice = productViewModel.DiscountPrice;
            existingProduct.BrandId = productViewModel.BrandId;
            existingProduct.CategoryId = productViewModel.CategoryId;
            existingProduct.CreatedAt = DateTime.Now;
            existingProduct.IsActive = true;
            existingProduct.Quantity = productViewModel.Quantity;
            _unitOfWork.Products.Update(existingProduct);
            _unitOfWork.Commit();
        }
    }
}