namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public int Quantity { get; init; }
        public string? Color { get; init; }
        public decimal Price { get; set; }
        public Guid ProductId { get; init; }
        public required string ProductName { get; init; }
    }
}
