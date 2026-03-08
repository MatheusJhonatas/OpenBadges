using BadgeCatalog.Application.Commands.CreateBadgeClass;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BadgeCatalog.Api.Controllers;

[ApiController]
[Route("api/badges")]
public class BadgesController : ControllerBase
{
    private readonly IMediator _mediator;
    public BadgesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBadgeClass(
        CreateBadgeClassCommand command, 
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return Created($"/api/badges/{id}", new { Id = id });
    }
}
