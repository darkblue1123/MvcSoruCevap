using Microsoft.AspNetCore.Mvc;

namespace MvcSoruCevap.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
