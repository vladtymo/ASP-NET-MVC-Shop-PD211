using Microsoft.EntityFrameworkCore;

namespace ShopMvcApp_PD211.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("connection...str");
        }
    }
}
