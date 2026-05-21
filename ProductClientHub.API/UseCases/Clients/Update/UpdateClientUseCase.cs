using Microsoft.AspNetCore.Authentication;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Update;
public class UpdateClientUseCase
{
    private readonly ProductClientHubDbContext _dbContext;
    public UpdateClientUseCase(ProductClientHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Execute(Guid cliendId, RequestClientJson request)
    {
        Validate(request);

        var entity = _dbContext.Clients.FirstOrDefault(client => client.Id == cliendId);
        if (entity == null)
            throw new NotFoundException("Cliente não encontrado.");

        entity.Name = request.Name;
        entity.Email = request.Email;

        _dbContext.Clients.Update(entity);
        _dbContext.SaveChanges();
    }

    private void Validate(RequestClientJson request)
    {
        var validator = new RequestClientValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }

    }
}

