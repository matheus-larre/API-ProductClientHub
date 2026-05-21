using Bogus;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionsBase;
using Xunit;

namespace ProductClientHub.Tests;

public class RegisterClientUseCaseTest
{
    [Fact]
    public void Success()
    {
        // Arrange
        var request = new Faker<RequestClientJson>()
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .Generate();

        var options = new DbContextOptionsBuilder<ProductClientHubDbContext>()
            .UseInMemoryDatabase(databaseName: "RegisterClientSuccess")
            .Options;

        var dbContext = new ProductClientHubDbContext(options);

        var validatorMock = new Mock<IValidator<RequestClientJson>>();
        validatorMock.Setup(v => v.Validate(request)).Returns(new FluentValidation.Results.ValidationResult());

        var useCase = new RegisterClientUseCase(dbContext, validatorMock.Object);

        // Act
        var result = useCase.Execute(request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Id.Should().NotBeEmpty();

        var clientInDb = dbContext.Clients.FirstOrDefault(c => c.Id == result.Id);
        clientInDb.Should().NotBeNull();
        clientInDb!.Name.Should().Be(request.Name);
        clientInDb!.Email.Should().Be(request.Email);
    }

    [Fact]
    public void Error_Name_Empty()
    {
        // Arrange
        var request = new Faker<RequestClientJson>()
            .RuleFor(c => c.Name, string.Empty)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .Generate();

        var options = new DbContextOptionsBuilder<ProductClientHubDbContext>()
            .UseInMemoryDatabase(databaseName: "RegisterClientError")
            .Options;

        var dbContext = new ProductClientHubDbContext(options);

        var validatorMock = new Mock<IValidator<RequestClientJson>>();
        var validationFailures = new List<FluentValidation.Results.ValidationFailure> 
        { 
            new FluentValidation.Results.ValidationFailure("Name", "O nome é obrigatório.") 
        };
        validatorMock.Setup(v => v.Validate(request)).Returns(new FluentValidation.Results.ValidationResult(validationFailures));

        var useCase = new RegisterClientUseCase(dbContext, validatorMock.Object);

        // Act
        var action = () => useCase.Execute(request);

        // Assert
        action.Should().Throw<ErrorOnValidationException>()
            .Where(ex => ex.GetErrors().Contains("O nome é obrigatório."));
    }
}
