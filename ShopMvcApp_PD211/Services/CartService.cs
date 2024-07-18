using ShopMvcApp_PD211.Extensions;

namespace ShopMvcApp_PD211.Services
{
    public interface ICartService
    {
        int GetCount();
    }

    public class CartService : ICartService
    {
        private readonly HttpContext httpContext;
        public CartService(IHttpContextAccessor contextAccessor)
        {
            httpContext = contextAccessor.HttpContext!;
        }

        public int GetCount()
        {
            // зчитуємо наявні елементи в корзині
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            // якщо елементів немає, тоді створюємо порожній список
            if (ids == null) return 0;

            return ids.Count;
        }
    }
}
