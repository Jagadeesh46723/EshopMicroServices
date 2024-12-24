using JasperFx.CodeGeneration.Frames;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id): IQuery<GetProductByResult>;
    public record GetProductByResult(Product Product);
    public class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByResult>
    {
        public async Task<GetProductByResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await session.LoadAsync<Product>(request.id, cancellationToken);

            return response is null ? throw new ProductNotFoundException() : new GetProductByResult(response);
        }
    }
}
