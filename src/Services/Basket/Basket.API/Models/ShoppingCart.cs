namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public required string UserName { get; init; }
        public IList<ShoppingCartItem> Items { get; set; } = [];

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart()
        {
            
        }

        public ShoppingCart(string Name)
        {
            UserName = Name;
        }
    }
}
