using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
