using ProductClientHub.API.Infrastructure;

namespace ProductClientHub.API.UseCases.Products.Delete;

public class DeleteProductUseCase
{
    private readonly ProductClientHubDbContext _dbContext;
    public DeleteProductUseCase(ProductClientHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Execute(Guid id)
    {
        var entity = _dbContext.Products.FirstOrDefault(product => product.Id == id);
        if (entity is null)
            throw new Exception("Produto não encontrado.");

        _dbContext.Products.Remove(entity);

        _dbContext.SaveChanges();
    }
}
