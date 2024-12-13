using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        public IActionResult Entry()
        {

            return View(_purchaseService.GetActiveSupplier());
        }
        [HttpPost]
        public IActionResult Entry(PurchaseViewModel purchaseViewModel) {
            _purchaseService.Create(purchaseViewModel);
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View(_purchaseService.RetrieveAll());
        }
        public IActionResult Delete(string Id)
        {
            _purchaseService.Delete(Id);
            return RedirectToAction("List");
        }
        public IActionResult Edit(string Id)
        {
            var purchaseEdit = _purchaseService.GetById(Id);
            // supplier ViewModel is populated dropdown list
            purchaseEdit.SupplierViewModels = _purchaseService.GetActiveSupplier().SupplierViewModels;
            return View(purchaseEdit);
        }
        [HttpPost]
        public IActionResult Update(PurchaseViewModel purchaseViewModel) {
            _purchaseService.Update(purchaseViewModel);
            return RedirectToAction("List");
        }

    }
}
