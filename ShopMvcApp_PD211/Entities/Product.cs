namespace ShopMvcApp_PD211.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }

        // ----- navigation properties
        public Category Category { get; set; }
    }
}