using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.UseCases.Products.Delete;
using ProductClientHub.API.UseCases.Products.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly RegisterProductUseCase _registerUseCase;
    private readonly DeleteProductUseCase _deleteUseCase;

    public ProductController(
        RegisterProductUseCase registerUseCase,
        DeleteProductUseCase deleteUseCase)
    {
        _registerUseCase = registerUseCase;
        _deleteUseCase = deleteUseCase;
    }

    [HttpPost]
    [Route("{clientId}")]
    [ProducesResponseType(typeof(ResponseShortProductJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
    public IActionResult Register([FromRoute] Guid clientId, [FromBody] RequestProductJson request)
    {
        var response = _registerUseCase.Execute(clientId, request);

        return Created(string.Empty, response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        _deleteUseCase.Execute(id);

        return NoContent();
    }
}