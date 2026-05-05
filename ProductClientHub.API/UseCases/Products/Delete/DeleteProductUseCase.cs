using ProductClientHub.API.Infrastructure;

namespace ProductClientHub.API.UseCases.Products.Delete;

public class DeleteProductUseCase
{
    public void Execute(Guid id)
    {
        var dbContext = new ProductClientHubDbContext();

        var entity = dbContext.Products.FirstOrDefault(product => product.Id == id);
        if (entity is null)
            throw new Exception("Produto não encontrado.");

        dbContext.Products.Remove(entity);

        dbContext.SaveChanges();
    }
}
