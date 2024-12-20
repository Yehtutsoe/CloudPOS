using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(SaleViewModel saleViewModel)
        {   
            _saleService.Create(saleViewModel);
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View(_saleService.RetrieveAll());
        }
        public IActionResult Edit(string Id)
        {
            return View(_saleService.GetById(Id));
        }
        public IActionResult Delete(string Id) {
            _saleService.Delete(Id);
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Update(SaleViewModel saleViewModel) {
            _saleService.Update(saleViewModel);
            return RedirectToAction("List");
        }
    }
}
