using FluentValidation;
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
    private readonly IValidator<RequestClientJson> _validator;
    public UpdateClientUseCase(ProductClientHubDbContext dbContext, IValidator<RequestClientJson> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
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
        var result = _validator.Validate(request);

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }

    }
}
