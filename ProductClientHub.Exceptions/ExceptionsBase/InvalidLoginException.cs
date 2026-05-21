using System.Net;

namespace ProductClientHub.Exceptions.ExceptionsBase;

public class InvalidLoginException : ProductClientHubException
{
    public InvalidLoginException() : base("E-mail e/ou senha inválidos.")
    {
    }

    public override List<string> GetErrors() => ["E-mail e/ou senha inválidos."];

    public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.Unauthorized;
}
