using Microsoft.AspNetCore.Mvc;

namespace SorteadorCore.Web.Controllers
{
    public class SorteiosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}