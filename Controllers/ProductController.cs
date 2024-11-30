using CloudPOS.Models.ViewModels;
using CloudPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        ErrorViewModel error = new ErrorViewModel();

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Entry()
        {
            return View(_productService.GetCategoryAndModel());
        }
        [HttpPost]
        public IActionResult Entry(ProductViewModel ui)
        {
            try
            {
                _productService.Create(ui);
                error.Message = "Successfull save the record to system";
                
            }
            catch (Exception)
            {
                error.Message = "Error occour,Unsuccessfull save the record to system";
                error.IsOccurError = true;
                throw;
            }
            return View();
        }

    }
}
