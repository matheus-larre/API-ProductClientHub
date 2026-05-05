using FluentValidation;
using ProductClientHub.Communication.Requests;

namespace ProductClientHub.API.UseCases.Products.SharedValidator;

public class RequestProductValidator : AbstractValidator<RequestProductJson>
{
    public RequestProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().WithMessage("O nome do pruduto é inválido.");
        RuleFor(product => product.Brand).NotEmpty().WithMessage("A marca do pruduto é inválida.");
        RuleFor(product => product.Price).GreaterThan(0).WithMessage("O preço do pruduto é inválido.");
    }
}
