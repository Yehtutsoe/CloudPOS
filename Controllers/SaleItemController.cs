using Microsoft.AspNetCore.Mvc;

namespace CloudPOS.Controllers
{
    public class SaleItemController : Controller
    {
        public IActionResult Entry()
        {
            return View();
        }
    }
}
