using Microsoft.AspNetCore.Mvc;

namespace StorageAPI
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
