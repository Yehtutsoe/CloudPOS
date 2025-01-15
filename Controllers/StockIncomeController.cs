using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class StockIncomeController : Controller
    {
        private readonly IStockIncomeService _stockIncome;

        public StockIncomeController(IStockIncomeService stockIncome)
        {
            _stockIncome = stockIncome;
        }
        public IActionResult Entry()
        {

            return View(_stockIncome.GetActiveSuppliersAndProducts());
        }
        [HttpPost]
        public IActionResult Entry(StockIncomeViewModel purchaseViewModel) {
            
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View(_stockIncome.RetrieveAll());
        }
        public IActionResult Delete(string Id)
        {
            _stockIncome.Delete(Id);
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id)
        {
            var purchaseEdit = _stockIncome.GetById(Id);
            // supplier and product  ViewModel are populated dropdown list
            // some code
            return View(purchaseEdit);
        }
        [HttpPost]
        public IActionResult Update(StockIncomeViewModel purchaseViewModel) {
            _stockIncome.Update(purchaseViewModel);
            return RedirectToAction("List");
        }

    }
}
