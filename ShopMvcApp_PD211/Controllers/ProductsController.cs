using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PD211.Data;
using ShopMvcApp_PD211.Entities;

namespace ShopMvcApp_PD211.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDbContext context = new();
        public ProductsController()
        {  
        }

        public IActionResult Index()
        {
            // ... load data from database ...
            var products = context.Products
                .Include(x => x.Category) // LEFT JOIN
                .ToList();

            return View(products);
        }

        // GET - open create page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST - create object in db
        [HttpPost]
        public IActionResult Create(Product model)
        {
            context.Products.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);

            if (product == null) return NotFound(); // 404

            context.Products.Remove(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
