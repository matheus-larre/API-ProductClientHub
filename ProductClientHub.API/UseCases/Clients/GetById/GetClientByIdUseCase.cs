using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.GetById;
public class GetClientByIdUseCase
{
    private readonly ProductClientHubDbContext _dbContext;
    public GetClientByIdUseCase(ProductClientHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ResponseClientJson Execute(Guid id)
    {
        var entity = _dbContext.Clients.Include(client => client.Products).FirstOrDefault(client => client.Id == id);
        if (entity == null)
            throw new NotFoundException("Cliente não encontrado.");

        return new ResponseClientJson
        {
            Id = id,
            Name = entity.Name,
            Email = entity.Email,
            Products = entity.Products.Select(product => new ResponseShortProductJson
            {
                Id = product.Id,
                Name = product.Name,
            }).ToList()
        };
    }
}
