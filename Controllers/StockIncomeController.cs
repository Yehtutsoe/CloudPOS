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

            return View(_stockIncomeService.GetStockIncomes());
        }
        [HttpPost]
        public IActionResult Entry(StockIncomeViewModel stockIncomeViewModel) {

            _stockIncomeService.Create(stockIncomeViewModel);
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View(_stockIncomeService.GetAll());
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
           
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(StockIncomeViewModel purchaseViewModel) {
            
            return RedirectToAction("List");
        }

    }
}
