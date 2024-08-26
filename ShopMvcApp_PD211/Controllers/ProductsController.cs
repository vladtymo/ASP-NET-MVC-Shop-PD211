using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Core.Dtos;
using Data.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ShopMvcApp_PD211.Extensions;

namespace ShopMvcApp_PD211.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ShopDbContext context;
        private readonly IFileService fileService;

        public ProductsController(IMapper mapper, ShopDbContext context, IFileService fileService)
        {
            this.mapper = mapper;
            this.context = context;
            this.fileService = fileService;
        }

        public IActionResult Index()
        {
            // ... load data from database ...
            var products = context.Products
                .Include(x => x.Category) // LEFT JOIN
                .ToList();

            return View(mapper.Map<List<ProductDto>>(products));
        }

        public IActionResult Details(int id)
        {
            var product = context.Products.Find(id);

            if (product == null) return NotFound();

            return View(mapper.Map<ProductDto>(product));
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
        public async Task<IActionResult> Create(ProductDto model)
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

            // save image and get file path
            if (model.Image != null)
                model.ImageUrl = await fileService.SaveProductImage(model.Image);

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
        public async Task<IActionResult> Edit(ProductDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                ViewBag.CreateMode = false;
                return View("Upsert", model);
            }

            if (model.Image != null)
                model.ImageUrl = await fileService.EditProductImage(model.ImageUrl, model.Image);

            context.Products.Update(mapper.Map<Product>(model));
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);

            if (product == null) return NotFound(); // 404

            if (product.ImageUrl != null) 
                fileService.DeleteProductImage(product.ImageUrl);

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
