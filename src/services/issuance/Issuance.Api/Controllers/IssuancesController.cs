using Issuance.Application.Commands.IssueBadge;
using Issuance.Application.Queries.GetAssertionById;
using Issuance.Application.Queries.GetAssertionByVerificationCode;
using Issuance.Application.Queries.GetIssuances;
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAssertionById(Guid id, CancellationToken cancellationToken)
    {
        var assertion = await _mediator.Send(new GetAssertionByIdQuery(id), cancellationToken);
        if (assertion == null)
        {
            return NotFound();
        }
        return Ok(assertion);
    }

    [HttpGet]
    public async Task<IActionResult> GetIssuances(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetIssuancesQuery(),
            cancellationToken);

        return Ok(result);
    }
[HttpGet("verify/{code}")]
public async Task<IActionResult> Verify(
    string code,
    CancellationToken cancellationToken)
{
    var result = await _mediator.Send(
        new GetAssertionByVerificationCodeQuery(code),
        cancellationToken);

    if (result is null)
    {
        return NotFound();
    }

    return Ok(result);
}

    [HttpPost("{id:guid}/revoke")]
    public async Task<IActionResult> Revoke(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new Application.Commands.RevokeAssertion.RevokeAssertionCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/unrevoke")]
    public async Task<IActionResult> Unrevoke(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new Application.Commands.ActiveAssertion.ActiveAssertionCommand(id), cancellationToken);
        return NoContent();
    }
}