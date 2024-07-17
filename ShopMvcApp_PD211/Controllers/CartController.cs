using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PD211.Data;
using ShopMvcApp_PD211.Dtos;
using ShopMvcApp_PD211.Extensions;

namespace ShopMvcApp_PD211.Controllers
{
    public class CartController : Controller
    {
        private ShopDbContext context = new();
        private readonly IMapper mapper;

        public CartController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        // відображає сторінку корзини із доданими продуктами
        public IActionResult Index()
        {
            var ids = HttpContext.Session.Get<List<int>>("cart_items") ?? new();

            var products = context.Products.Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToList();

            return View(mapper.Map<List<ProductDto>>(products));
        }

        // додає продукт в корзину
        public IActionResult Add(int id)
        {
            // зчитуємо наявні елементи в корзині
            var ids = HttpContext.Session.Get<List<int>>("cart_items");

            // якщо елементів немає, тоді створюємо порожній список
            if (ids == null) ids = new();
            // додаємо новий елемент
            ids.Add(id);

            // зберігаємо оновлений список корзини в cookies
            HttpContext.Session.Set("cart_items", ids);

            return RedirectToAction("Index");
        }

        // видаляє продукт з корзини
        public IActionResult Remove()
        {
            return View();
        }
    }
}