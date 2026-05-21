using ProductClientHub.API.Infrastructure.Security;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;
using Microsoft.EntityFrameworkCore;

namespace ProductClientHub.API.UseCases.Login;

public class DoLoginUseCase
{
    private readonly ProductClientHubDbContext _dbContext;
    private readonly TokenController _tokenController;

    public DoLoginUseCase(ProductClientHubDbContext dbContext, TokenController tokenController)
    {
        _dbContext = dbContext;
        _tokenController = tokenController;
    }

    public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || BCrypt.Net.BCrypt.Verify(request.Password, user.Password) == false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseLoginJson
        {
            Name = user.Name,
            Token = _tokenController.GenerateToken(user.Email)
        };
    }
}
