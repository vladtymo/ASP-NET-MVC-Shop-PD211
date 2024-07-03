using Microsoft.AspNetCore.Mvc;

namespace ShopMvcApp_PD211.Controllers
{
    public class ProductsController : Controller
    {
        List<Product> products;
        public ProductsController()
        {
            products = new List<Product>()
            {
                new Product() { Id = 1, Title = "iPhone X", Category = "Electronics", Discount = 10, Price = 389, Quantity = 4 },
                new Product() { Id = 2, Title = "Samsung S4", Category = "Electronics", Discount = 0, Price = 440, Quantity = 0 }
            };
        }

        public IActionResult Index()
        {
            // ... load data from database ...

            return View(products);
        }
    }
}
