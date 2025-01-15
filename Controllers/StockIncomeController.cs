using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IStockIncomeService _stockIncomeService;

        public PurchaseController(IStockIncomeService purchaseService)
        {
            _stockIncomeService = purchaseService;
        }
        public IActionResult Entry()
        {

            return View(_stockIncomeService.GetActiveSupplier());
        }
        [HttpPost]
        public IActionResult Entry(StockIncomeViewModel purchaseViewModel) {
            
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View(_stockIncomeService.RetrieveAll());
        }
        public IActionResult Delete(string Id)
        {
            _stockIncomeService.Delete(Id);
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id)
        {
            var purchaseEdit = _stockIncomeService.GetById(Id);
            // supplier ViewModel is populated dropdown list
            purchaseEdit.SupplierViewModels = _stockIncomeService.GetActiveSupplier().SupplierViewModels;
            return View(purchaseEdit);
        }
        [HttpPost]
        public IActionResult Update(StockIncomeViewModel purchaseViewModel) {
            _stockIncomeService.Update(purchaseViewModel);
            return RedirectToAction("List");
        }

    }
}
