using Issuance.Application.Commands.IssueBadge;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Issuance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuancesController : ControllerBase
{
    private readonly IMediator _mediator;
    public IssuancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> IssueBadge(IssueBadgeCommand command,
        CancellationToken cancellationToken)
    {
        var assertionId = await _mediator.Send(command, cancellationToken);
        return Created($"/api/issuances/{assertionId}", new { id = assertionId });
    }
    
}