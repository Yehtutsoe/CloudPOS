using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services.ReportingServices
{ 
    public class SaleItemReportService : ISaleItemReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleItemReportService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IList<SaleItemReportViewModel> GetSaleItemReport(string fromDate, string toDate, string productId)
        {
            DateTime? fromDateValue = null;
            DateTime? toDateValue = null;
            if(!string.IsNullOrEmpty(fromDate)) fromDateValue = DateTime.Parse(fromDate);
            if (!string.IsNullOrEmpty(toDate)) toDateValue = DateTime.Parse(toDate);

            if(productId is not null && productId != "Select Product")
            {
                var saleItemQuery = (from si in _unitOfWork.SaleItems.GetAll()
                                     join sa in _unitOfWork.Sales.GetAll()
                                     on si.SaleId equals sa.Id
                                     join p in _unitOfWork.Products.GetAll()
                                     on si.ProductId equals p.Id
                                     join c in _unitOfWork.Categories.GetAll()
                                     on p.CategoryId equals c.Id
                                     where (fromDateValue == null || sa.SaleDate >= fromDateValue) &&
                                     (toDateValue == null || sa.SaleDate <= toDateValue)
                                     select new SaleItemReportViewModel
                                     {
                                         SaleDate = sa.SaleDate.ToString("yyyy-MM-dd"),
                                         SaleType = sa.SaleType,
                                         VoucherNo = sa.VoucherNo,
                                         CategoryName = c.Name,
                                         ProductName = p.Name,
                                         Quantity = si.Quantity,
                                         Price = si.Price,
                                         Amount = si.SaleAmount,
                                         DiscountAmount = sa.DiscountAmount,
                                         DiscountPercent = sa.DiscountPercent,
                                         Total = si.TotalPrice
                                     }
                                     );
                return saleItemQuery.ToList();
            }
            else
            {
                var saleItemQuery = (from si in _unitOfWork.SaleItems.GetAll()
                                     join sa in _unitOfWork.Sales.GetAll()
                                     on si.SaleId equals sa.Id
                                     join p in _unitOfWork.Products.GetAll()
                                     on si.ProductId equals p.Id
                                     join c in _unitOfWork.Categories.GetAll()
                                     on p.CategoryId equals c.Id
                                     where (fromDateValue == null || sa.SaleDate >= fromDateValue) &&
                                     (toDateValue == null || sa.SaleDate <= toDateValue)
                                     select new SaleItemReportViewModel
                                     {
                                         SaleDate = sa.SaleDate.ToString("yyyy-MM-dd"),
                                         SaleType = sa.SaleType,
                                         VoucherNo = sa.VoucherNo,
                                         CategoryName = c.Name,
                                         ProductName = p.Name,
                                         Quantity = si.Quantity,
                                         Price = si.Price,
                                         Amount = si.SaleAmount,
                                         DiscountAmount = sa.DiscountAmount,
                                         DiscountPercent = sa.DiscountPercent,
                                         Total = si.TotalPrice
                                     }
                                    );
                return saleItemQuery.ToList();
            }

        }
    }
}
