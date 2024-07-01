using Microsoft.AspNetCore.Mvc;
using ShopMvcApp_PD211.Models;
using System.Diagnostics;

namespace ShopMvcApp_PD211.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            return View(); // search for view in: ~/Views/Home/Index
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            // your logic...
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
