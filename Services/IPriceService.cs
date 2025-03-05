using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IPriceService
    {
        void Create(PriceViewModel priceViewModel);
        IEnumerable<PriceViewModel> GetAll();
        PriceViewModel GetById(string Id);
        void Update(PriceViewModel priceViewModel);
        void Delete(string Id);
        IEnumerable<PriceViewModel> GetPrices();
        ProductPriceViewModel ProductDetailsByBarCode(string BarCode);
    }
}
