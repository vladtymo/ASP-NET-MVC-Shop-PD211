﻿using Core.Services;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ShopMvcApp_PD211.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ShopDbContext context;
        private readonly ICartService cartService;

        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public OrdersController(ShopDbContext context, ICartService cartService)
        {
            this.context = context;
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            var orders = context.Orders
                                .Include(x => x.User)
                                .Where(x => x.UserId == CurrentUserId)
                                .ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            var order = new Order()
            {
                CreatedAt = DateTime.Now,
                UserId = CurrentUserId,
                Products = cartService.GetProductsEntity()
            };

            context.Orders.Add(order);
            context.SaveChanges();

            cartService.Clear();

            return RedirectToAction("Index");
        }
    }
}
