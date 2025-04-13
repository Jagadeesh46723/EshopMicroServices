using Basket.API.Data;
using Discount.Grpc;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class StoreBasketHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountService) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await DeductDiscount(request.Cart, discountService);
            _ = await basketRepository.StoreBasket(request.Cart, cancellationToken);
            return new StoreBasketResult(request.Cart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart cart, DiscountProtoService.DiscountProtoServiceClient discountService)
        {
            foreach (var item in cart.Items)
            {
                var discount = await discountService.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
                item.Price -= discount.Amount;
            }
        }
    }
}
