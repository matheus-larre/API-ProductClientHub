using ProductClientHub.API.Infrastructure;

namespace ProductClientHub.API.UseCases.Clients.Delete;

public class DeleteClientUseCase
{
    private readonly ProductClientHubDbContext _dbContext;
    public DeleteClientUseCase(ProductClientHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Execute(Guid id)
    {
        var entity = _dbContext.Clients.FirstOrDefault(client => client.Id == id);
        if (entity is null)
            throw new Exception("Cliente não encontrado.");

        _dbContext.Clients.Remove(entity);

        _dbContext.SaveChanges();
    }
}
