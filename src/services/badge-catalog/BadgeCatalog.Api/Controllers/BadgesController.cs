using BadgeCatalog.Application.Commands.ActiveBadgeClass;
using BadgeCatalog.Application.Commands.CreateBadgeClass;
using BadgeCatalog.Application.Commands.DeactivateBadgeClass;
using BadgeCatalog.Application.Commands.UpdateBadgeClass;
using BadgeCatalog.Application.Queries.GetAllBadges;
using BadgeCatalog.Application.Queries.GetBadgeBySlug;
using BadgeCatalog.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet]
    public async Task<IActionResult> GettAllBadges(
        [FromQuery] bool? active,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllBadgesQuery(active), cancellationToken);
        return Ok(result);
    }
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBadgeBySlug(
        string slug,
        CancellationToken cancellationToken)
    {
        var badge = await _mediator.Send(new GetBadgeBySlugQuery(slug), cancellationToken);

        if(badge is null)
        {
            return NotFound(new { message = "Não existe badge com esse slug." });
        }
        return Ok(badge);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> DeactivateBadgeClass(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeactivateBadgeClassCommand(id), cancellationToken);

        if (!result)
        {
            return NotFound(new { message = "Não existe badge com esse id." });
        }
        return NoContent();
    }
    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> ActivateBadgeClass(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ActiveBadgeClassCommand(id), cancellationToken);

        if (!result)
        {
            return NotFound(new { message = "Não existe badge com esse id." });
        }
        return NoContent();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBadge(
        Guid id,
        UpdateBadgeClassCommand command,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var updated = await _mediator.Send(command, cancellationToken);

        if (!updated)
        {
            return NotFound(new { message = "Badge not found." });
        }

        return NoContent();
        }catch (DbUpdateConcurrencyException)
        {
            return Conflict(new { message = "Esta badge foi modificada por outro usuário." });
        }
        catch (ConcurrencyException  ex)
        {
             return Conflict(new
    {
        message = ex.Message
    });
        }
    }
}
