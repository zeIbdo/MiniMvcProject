using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MiniMvcProject.ADMIN.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
