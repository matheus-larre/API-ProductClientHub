using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.UseCases.Clients.GetAll;
public class GetAllClientsUseCase
{
    private readonly ProductClientHubDbContext _dbContext;
    public GetAllClientsUseCase(ProductClientHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ResponseAllClientsJson Execute()
    {
        var clients = _dbContext.Clients.ToList();

        return new ResponseAllClientsJson
        {
            Clients = clients.Select(client => new ResponseShortClientJson
            {
                Id = client.Id,
                Name = client.Name,
            }).ToList()
        };
    }
}

