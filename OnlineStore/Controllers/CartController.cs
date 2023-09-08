using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Areas.Identity.Data;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreDbContext db;
        public CartController(ILogger<HomeController> logger, UserManager<AppUser> userManager, StoreDbContext _db)
        {
            _logger = logger;
            _userManager = userManager;
            db = _db;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserId(this.User);
            foreach (var us in db.Users)
            {
                if (us.Id == user)
                {
                    ViewBag.FirstName = us.FirstName;
                    ViewBag.LastName = us.LastName;
                    ViewBag.Id = us.Id;
                }
            }
            var items = db.Carts.Where(c => c.UserId == user).ToList();
            if (items.Count == 0)
            {
                return RedirectToAction("EmptyCart");
            }
            Dictionary<KeyValuePair<string,string>, KeyValuePair<int, double>> temp = new Dictionary<KeyValuePair<string, string>, KeyValuePair<int, double>>();
            double totalSum = 0.0;
            foreach (var item in items)
            {
                var prod = db.Products.Find(item.ProductId);
                double price = prod.UnitPrice - (prod.UnitPrice * (prod.Discount * 1.0 / 100));
                totalSum += price * item.Quantity;

                temp.Add(new KeyValuePair<string, string>(prod.Name, item.SellerId), new KeyValuePair<int, double>(item.Quantity, price * item.Quantity));
            }
            ViewBag.Sum = totalSum;
            return View(temp);

        }
        public IActionResult Add(int id, int RequiredQuantity, string SellerId)
        {
            var pro = db.Products.Find(id);
            var user = _userManager.GetUserId(this.User);
            var cart = db.Carts.FirstOrDefault(c => c.UserId == user && c.ProductId == id && c.SellerId == SellerId);
            if (cart == null)
            {
                cart = new Cart()
                {
                    ProductId = id,
                    UserId = user,
                    Quantity = RequiredQuantity,
                    SellerId = SellerId
                };
                db.Carts.Add(cart);
            }
            else
            {
                cart.Quantity += RequiredQuantity;
                db.Update(cart);
            }
            pro.Quantity -= RequiredQuantity;
            db.Update(pro);
            db.SaveChanges();
            return View();

        }
        public IActionResult Confirm(string Id)
        {
            var items = db.Carts.Where(c => c.UserId == Id);
            foreach (var item in items)
            {
                db.Carts.Remove(item);
            }
            db.SaveChanges();
            return View();

        }
        public IActionResult Cancel(string Id)
        {
            var items = db.Carts.Where(c => c.UserId == Id);
            foreach (var item in items)
            {
                var prod = db.Products.FirstOrDefault(c => c.Id == item.ProductId);
                prod.Quantity += item.Quantity;
                db.Update(prod);
                db.Carts.Remove(item);
            }
            db.SaveChanges();
            return View();
        }
        public IActionResult EmptyCart()
        {
            return View();
        }

    }
}
