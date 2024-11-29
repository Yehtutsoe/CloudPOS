using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class PhoneModelController : Controller
    {
        private readonly IPhoneModelService _modelService;
        ErrorViewModel error = new ErrorViewModel();

        public PhoneModelController(IPhoneModelService modelService)
        {
            _modelService = modelService;
        }
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(PhoneModelViewModel modelViewModel)
        {
            try
            {
                _modelService.Create(modelViewModel);
                error.Message = "Successfully Save the record to the system!";
                
            }
            catch
            {
                error.Message = "Error ,Cann't save to the system";
                error.IsOccurError = true;
            }
            ViewBag.Msg = error;
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            return View(_modelService.RetrieveAll());
        }
        public IActionResult Edit(string Id)
        {
            return View(_modelService.GetById(Id));
        }
        [HttpPost]
        public IActionResult Update(PhoneModelViewModel model) {

            try
            {
                _modelService.Update(model);
                TempData["Msg"] = "Successfully update to the system!";
                TempData["IsOccourError"] = false;
            }
            catch (Exception)
            {
                TempData["Msg"] = "Can not update to the system,Error Occour!";
                TempData["IsOccourError"] = true;
                throw;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(string Id)
        {
            try
            {
                _modelService.Delete(Id);
                TempData["Msg"] = "Successfully Delete form System";
                TempData["IsOccourError"] = false;
            }
            catch (Exception)
            {
                TempData["Msg"] = "Can not Delete from System,Error occour!";
                TempData["IsOccourError"] = true;
                throw;
            }
            return RedirectToAction("List");
        }
    }
}
