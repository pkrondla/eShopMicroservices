using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id)
        : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSucess);
    public class DeleteCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");            
        }
    }
    internal class DeleteProductCommandHandler
         (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

            var product = session.LoadAsync<Product>(command.Id);

            if (product == null) {

                throw new ProductNotFoundException(command.Id);
            }
            
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync();

            return new DeleteProductResult(true);
        }
    }
}
