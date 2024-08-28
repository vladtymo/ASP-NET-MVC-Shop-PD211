using Core.Services;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender emailSender;

        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        private string CurrentUserEmail => User.FindFirstValue(ClaimTypes.Email)!;

        public OrdersController(
            ShopDbContext context, 
            ICartService cartService,
            IEmailSender emailSender)
        {
            this.context = context;
            this.cartService = cartService;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var orders = context.Orders
                                .Include(x => x.User)
                                .Where(x => x.UserId == CurrentUserId)
                                .ToList();
            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            var products = cartService.GetProductsEntity();

            var order = new Order()
            {
                CreatedAt = DateTime.Now,
                UserId = CurrentUserId,
                Products = cartService.GetProductsEntity()
            };

            context.Orders.Add(order);
            context.SaveChanges();

            var totalPrice = products.Sum(x => x.Price);

            // send email of order to the client
            await emailSender.SendEmailAsync(CurrentUserEmail, $"New Order #{order.Id}", $"<p>Total Price: {totalPrice}$</p>");

            cartService.Clear();

            return RedirectToAction("Index");
        }
    }
}
