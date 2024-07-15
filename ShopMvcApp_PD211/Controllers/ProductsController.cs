using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PD211.Data;
using ShopMvcApp_PD211.Dtos;
using ShopMvcApp_PD211.Entities;

namespace ShopMvcApp_PD211.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDbContext context = new();
        private readonly IMapper mapper;

        public ProductsController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            // ... load data from database ...
            var products = context.Products
                .Include(x => x.Category) // LEFT JOIN
                .ToList();

            return View(mapper.Map<List<ProductDto>>(products));
        }

        // GET - open create page
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            ViewBag.CreateMode = true;
            return View("Upsert");
        }

        // POST - create object in db
        [HttpPost]
        public IActionResult Create(ProductDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                ViewBag.CreateMode = true;
                return View("Upsert", model);
            }

            // 1 - manual mapping
            //var entity = new Product
            //{
            //    Title = model.Title,
            //    CategoryId = model.CategoryId,
            //    Description = model.Description,
            //    Discount = model.Discount,
            //    ImageUrl = model.ImageUrl,
            //    Price = model.Price,
            //    Quantity = model.Quantity
            //};
            // 2 - using auto mapper
            var entity = mapper.Map<Product>(model);

            context.Products.Add(entity);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);

            if (product == null) return NotFound();

            LoadCategories();
            ViewBag.CreateMode = false;
            return View("Upsert", mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public IActionResult Edit(ProductDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                ViewBag.CreateMode = false;
                return View("Upsert", model);
            }

            context.Products.Update(mapper.Map<Product>(model));
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

        private void LoadCategories()
        {
            // ViewBag - collection of properties that is accessible in View
            ViewBag.Categories = new SelectList(context.Categories.ToList(), "Id", "Name");
        }
    }
}
