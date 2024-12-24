namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, Decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid id);
    public class CreateProductHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = command.Adapt<Product>();
            documentSession.Store(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
