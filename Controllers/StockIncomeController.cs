using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class StockIncomeController : Controller
    {
        private readonly IStockIncomeService _stockIncomeService;

        public StockIncomeController(IStockIncomeService stockIncomeService)
        {
            _stockIncomeService = stockIncomeService;
        }
        public IActionResult Entry()
        {

            return View(_stockIncomeService.GetActiveSuppliersAndProducts());
        }
        [HttpPost]
        public IActionResult Entry(StockIncomeViewModel stockIncomeViewModel) {

            _stockIncomeService.Create(stockIncomeViewModel);
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View(_stockIncomeService.RetrieveAll());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string Id)
        {
            _stockIncomeService.Delete(Id);
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string Id)
        {
            var purchaseEdit = _stockIncomeService.GetById(Id);
            // supplier and product  ViewModel are populated dropdown list
            // some code
            return View(purchaseEdit);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(StockIncomeViewModel purchaseViewModel) {
            _stockIncomeService.Update(purchaseViewModel);
            return RedirectToAction("List");
        }

    }
}
