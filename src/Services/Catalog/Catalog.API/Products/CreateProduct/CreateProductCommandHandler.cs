using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Catergory, string Description, string ImageFile, Decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = command.Adapt<Product>();
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
