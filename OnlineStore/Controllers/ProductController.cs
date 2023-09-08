using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Areas.Identity.Data;
using OnlineStore.Data;
using OnlineStore.Data.Enums;
using OnlineStore.Models;
using System.Diagnostics;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly StoreDbContext db;
        public ProductController(ILogger<HomeController> logger, UserManager<AppUser> userManager, StoreDbContext _db)
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
                    ViewBag.Name = us.FirstName;
                    ViewBag.Type = us.UserType;
                    ViewBag.Id = us.Id;
                }
            }

            return View(db.Products.ToList());
        }

        public IActionResult New()
        {
            var user = _userManager.GetUserId(this.User);
            foreach (var us in db.Users)
            {
                if (us.Id == user)
                {
                    ViewBag.ID = us.Id;
                    ViewBag.Type = us.UserType;
                }
            }
            if (ViewBag.Type == UserType.Seller)
                return View();
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult New(Product p, IFormFile? Imagefile)
        {
            if (ModelState.IsValid)
            {
                if (Imagefile != null)
                {
                    var memStream = new MemoryStream();
                    Imagefile.CopyTo(memStream);
                    p.Image = memStream.ToArray();
                }
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var user = _userManager.GetUserId(this.User);
            foreach (var us in db.Users)
            {
                if (us.Id == user)
                {
                    ViewBag.ID = us.Id;
                }
            }
            return View("New", p);
        }

        public IActionResult Details(int id)
        {
            var user = _userManager.GetUserId(this.User);
            var pro = db.Products.Find(id);
            foreach (var us in db.Users)
            {
                if (us.Id == pro.SellerId)
                {
                    ViewBag.FirstName = us.FirstName;
                    ViewBag.LastName = us.LastName;
                    ViewBag.SellerId = us.Id;
                    
                }
                if (us.Id == user)
                {
                    ViewBag.Id = us.Id;
                    ViewBag.Type = us.UserType;
                }
            }
            return View(pro);
        }

        public IActionResult Edit(int id)
        {
            var user = _userManager.GetUserId(this.User);
            var prod = db.Products.FirstOrDefault(p => p.Id == id);
            foreach (var us in db.Users)
            {
                if (prod.SellerId == us.Id)
                {
                    ViewBag.FirstName = us.FirstName;
                    ViewBag.LastName = us.LastName;
                    ViewBag.Id = us.Id;
                }
            }
            if (ViewBag.Id == user)
                return View(db.Products.Find(id));
            else
                return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Edit(Product newPro, IFormFile? Imagefile)
        {
            try
            {
                if (newPro.Id != 0)
                {
                    if (ModelState.IsValid)
                    {
                        var existingPro = db.Products.Find(newPro.Id);
                        if (Imagefile != null)
                        {
                            var memStream = new MemoryStream();
                            Imagefile.CopyTo(memStream);
                            newPro.Image = memStream.ToArray();
                        }
                        else
                        {
                            newPro.Image = existingPro.Image;
                        }
                        existingPro.Name = newPro.Name;
                        existingPro.UnitPrice = newPro.UnitPrice;
                        existingPro.Quantity = newPro.Quantity;
                        existingPro.Description = newPro.Description;
                        existingPro.Discount = newPro.Discount;
                        existingPro.Category = newPro.Category;
                        existingPro.Image = newPro.Image;

                        db.Update(existingPro);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Edit", newPro);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Edit", newPro);
            }
        }

        public IActionResult Delete(int id)
        {
            var user = _userManager.GetUserId(this.User);
            var prod = db.Products.FirstOrDefault(p => p.Id == id);
            foreach (var us in db.Users)
            {
                if (us.Id == prod.SellerId)
                {
                    ViewBag.Id = us.Id;
                    ViewBag.FirstName = us.FirstName;
                    ViewBag.LastName = us.LastName;
                    ViewBag.Id = us.Id;
                }
            }
            if (ViewBag.Id == user)
                return View(db.Products.Find(id));
            else
                return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Delete(Product p, int Id)
        {
            try
            {
                Product pro = db.Products.Find(Id);
                db.Remove(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult SellerProducts(string Id)
        {
            var items = db.Products.Where(p => p.SellerId == Id).ToList();
            var user = _userManager.GetUserId(this.User);
            foreach (var us in db.Users)
            {
                if (us.Id == Id)
                {
                    ViewBag.FirstName = us.FirstName;
                    ViewBag.LastName = us.LastName;
                }
                if(us.Id == user)
                {
                    ViewBag.Id = us.Id;
                }
            }
            return View(items);
        }
        public IActionResult Search(string? query)
        {
            var user = _userManager.GetUserId(this.User);
            foreach (var us in db.Users)
            {
                if (us.Id == user)
                {
                    ViewBag.Name = us.FirstName;
                    ViewBag.Type = us.UserType;
                    ViewBag.Id = us.Id;
                }
            }
            var items = new List<Product>();
            if (query == null)
                items = db.Products.ToList();
            else
            {
                items = db.Products.Where(p => p.Name.ToLower().Contains(query.ToLower()) || p.Description.ToLower().Contains(query.ToLower()) || p.Category.ToLower().Contains(query.ToLower())).ToList();
                if (items.Count == 0) 
                {
                    return View("NotFound");
                }
            }
            return View("Index", items);
        }
    }
}
