using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Products.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Products.Register;

public class RegisterProductUseCase
{
    private readonly ProductClientHubDbContext _dbContext;
    public RegisterProductUseCase(ProductClientHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ResponseShortProductJson Execute(Guid cliendId, RequestProductJson request)
    {
        Validate(cliendId, request);

        var entity = new Product
        {
            Name = request.Name,
            Brand = request.Brand,
            Price = request.Price,
            ClientId = cliendId,
        };

        _dbContext.Products.Add(entity);

        _dbContext.SaveChanges();

        return new ResponseShortProductJson
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    private void Validate(Guid cliendId, RequestProductJson request)
    {
        var clientExist = _dbContext.Clients.Any(client => client.Id == cliendId);
        if (clientExist == false)
            throw new NotFoundException("Cliente não existe.");

        var validator = new RequestProductValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}
