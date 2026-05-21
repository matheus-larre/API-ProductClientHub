using FluentValidation;
using ProductClientHub.Communication.Requests;

namespace ProductClientHub.API.UseCases.Products.SharedValidator;

public class RequestProductValidator : AbstractValidator<RequestProductJson>
{
    public RequestProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.");

        RuleFor(product => product.Brand)
            .NotEmpty().WithMessage("A marca do produto é obrigatória.")
            .MaximumLength(50).WithMessage("A marca deve ter no máximo 50 caracteres.");

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");
    }
}
