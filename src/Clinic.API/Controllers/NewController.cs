using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    public class NewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
