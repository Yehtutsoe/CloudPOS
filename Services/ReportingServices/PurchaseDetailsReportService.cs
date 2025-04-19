using CloudPOS.Models.ViewModels;
using CloudPOS.UnitOfWork;

namespace CloudPOS.Services.ReportingServices
{
    public class PurchaseDetailsReportService : IPurchaseDetailsReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseDetailsReportService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IList<PurchaseDetailsReportViewModel> GetPruchaseReport(string fromDate, string toDate,string productId)
        {
            DateTime? fromDateValue = null;
            DateTime? toDateValue = null;

            if(!string.IsNullOrEmpty(fromDate)) fromDateValue = DateTime.Parse(fromDate);
            if(!string.IsNullOrEmpty(toDate)) toDateValue = DateTime.Parse(toDate);  

            if(productId is not null && productId != "Select Product")
            {
                var purchaseDetailsQuery = (from pd in _unitOfWork.PurchaseDetails.GetAll()
                                            join p in _unitOfWork.Purchases.GetAll()
                                            on pd.PurchaseId equals p.Id
                                            join s in _unitOfWork.Suppliers.GetAll()
                                            on p.SupplierId equals s.Id
                                            join pro in _unitOfWork.Products.GetAll()
                                            on pd.ProductId equals pro.Id
                                            join c in _unitOfWork.Categories.GetAll()
                                            on pro.CategoryId equals c.Id
                                            where (fromDateValue == null || p.PurchaseDate >= fromDateValue) &&
                                            (toDateValue == null || p.PurchaseDate <= toDateValue) &&
                                            (productId == pd.ProductId)
                                            select new PurchaseDetailsReportViewModel
                                            {
                                                PurchaseDate = p.PurchaseDate.ToString("yyyy-MM-dd"),
                                                PurchaseVoucherNo = p.VoucherNo,
                                                CategoryName = c.Name,
                                                Price = pd.Price,
                                                TotalPrice = pd.TotalPrice,
                                                ProductName = pro.Name,
                                                Quantity = pd.Quantity,
                                                SupplierName = s.Name
                                            }).ToList();
                return purchaseDetailsQuery;
            }
            else
            {
                var purchaseDetailsQuery = (from pd in _unitOfWork.PurchaseDetails.GetAll()
                                            join p in _unitOfWork.Purchases.GetAll()
                                            on pd.PurchaseId equals p.Id
                                            join s in _unitOfWork.Suppliers.GetAll()
                                            on p.SupplierId equals s.Id
                                            join pro in _unitOfWork.Products.GetAll()
                                            on pd.ProductId equals pro.Id
                                            join c in _unitOfWork.Categories.GetAll()
                                            on pro.CategoryId equals c.Id
                                            where (fromDateValue == null || p.PurchaseDate >= fromDateValue) &&
                                            (toDateValue == null || p.PurchaseDate <= toDateValue)
                                            
                                            select new PurchaseDetailsReportViewModel
                                            {
                                                PurchaseDate = p.PurchaseDate.ToString("yyyy-MM-dd"),
                                                PurchaseVoucherNo = p.VoucherNo,
                                                CategoryName = c.Name,
                                                Price = pd.Price,
                                                TotalPrice = pd.TotalPrice,
                                                ProductName = pro.Name,
                                                Quantity = pd.Quantity,
                                                SupplierName = s.Name
                                            }).ToList();
                return purchaseDetailsQuery;
            }

        }
    }
}
