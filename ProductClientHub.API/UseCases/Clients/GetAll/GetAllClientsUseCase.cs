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

    public ResponseAllClientsJson Execute(int pageNumber)
    {
        const int pageSize = 10;

        var query = _dbContext.Clients.AsQueryable();

        var totalCount = query.Count();

        var clients = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new ResponseAllClientsJson
        {
            Pagination = new ResponsePaginationJson
            {
                PageNumber = pageNumber,
                TotalPages = totalPages,
                TotalCount = totalCount
            },
            Clients = clients.Select(client => new ResponseShortClientJson
            {
                Id = client.Id,
                Name = client.Name,
            }).ToList()
        };
    }
}

