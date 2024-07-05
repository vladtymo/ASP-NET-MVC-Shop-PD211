using Microsoft.AspNetCore.Mvc;
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
            var products = context.Products.ToList();

            return View(products);
        }
    }
}
