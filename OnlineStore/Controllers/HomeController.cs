using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Areas.Identity.Data;
using OnlineStore.Models;
using System.Diagnostics;

namespace OnlineStore.Controllers
{
    //[Authorize]    //to prevent unauthorized users to see home page
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger,UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            
            return RedirectToAction("Index","Product");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}