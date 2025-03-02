using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly IPurchaseDetailService _detailService;

        public PurchaseController(IPurchaseService purchaseService,IProductService productService,ISupplierService supplierService,IPurchaseDetailService detailService)
        {
            this._purchaseService = purchaseService;
            this._productService = productService;
            this._supplierService = supplierService;
            this._detailService = detailService;
        }
        public  IActionResult Entry()
        {
            BindProductData();
            BindSupplierData();
            BindPurchaseData();
            var model = new PurchaseWithDetailViewModel();
            return View(model);
        }

        private void BindPurchaseData()
        {
           var purchases =  _purchaseService.GetAll();
            ViewBag.Purchases = purchases;
        }

        private void BindSupplierData()
        {
            var suppliers = _supplierService.GetSuppliers();
            ViewBag.Suppliers = suppliers;
        }

        private void BindProductData()
        {
            var products = _productService.GetProducts();
            ViewBag.Products = products;    
        }
        [HttpPost]
        public IActionResult Entry(PurchaseWithDetailViewModel model)
        {
            try
            {
                _purchaseService.Create(model);
                ViewData["Info"] = "Successfull Save the record";
                ViewData["Status"] = true;
            }
            catch (Exception)
            {
                ViewData["Info"] = "Error occour to save the system";
                ViewData["Status"] = false;
               
            }
            BindProductData();
            BindSupplierData();
            return View(model);
        }
        public IActionResult List(DateTime? fromDate=null, DateTime? toDate=null,string? supplierId = null,string? productId = null)
        {
            var purchase = _purchaseService.GetAll(fromDate, toDate, supplierId, productId);
            BindSupplierData();
            BindPurchaseData();
            return View(purchase);
        }
        public IActionResult Delete(string Id)
        {
            try
            {
                _purchaseService.Delete(Id);
                TempData["Info"] = "Successfully delete data to the system";
                TempData["status"] = true;
            }
            catch (Exception)
            {

                TempData["Info"] = "Occour error to delete the data to the system";
                TempData["status"] = false;
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id)
        {
            return View(_productService.GetById(Id));
        }
    }
}
