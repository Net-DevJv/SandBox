using Microsoft.AspNetCore.Mvc;

namespace SnackHub.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
