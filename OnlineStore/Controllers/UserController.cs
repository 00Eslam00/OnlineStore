using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Areas.Identity.Data;
using OnlineStore.Data;

namespace OnlineStore.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreDbContext db;
        public UserController(ILogger<HomeController> logger, UserManager<AppUser> userManager, StoreDbContext _db)
        {
            _logger = logger;
            _userManager = userManager;
            db = _db;
        }

        public IActionResult ManageProduct(string Id)
        {
            var products = db.Products.Where(p => p.SellerId == Id).ToList();

            foreach (var us in db.Users)
            {
                if (us.Id == Id)
                {
                    ViewBag.FirstName = us.FirstName;
                    ViewBag.LastName = us.LastName;
                }
            }

            return View(products);
        }
    }
}
