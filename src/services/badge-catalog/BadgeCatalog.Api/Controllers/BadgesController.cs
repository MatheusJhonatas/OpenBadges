using System.Runtime.InteropServices;
using BadgeCatalog.Api.Requests;
using BadgeCatalog.Application.Commands.ActiveBadgeClass;
using BadgeCatalog.Application.Commands.CreateBadgeClass;
using BadgeCatalog.Application.Commands.DeactivateBadgeClass;
using BadgeCatalog.Application.Commands.UpdateBadgeClass;
using BadgeCatalog.Application.Queries.GetAllBadges;
using BadgeCatalog.Application.Queries.GetBadgeBySlug;
using BadgeCatalog.Domain.Exceptions;
using BadgeCatalog.Ports.Models;
using BadgeCatalog.Ports.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadgeCatalog.Api.Controllers;

[ApiController]
[Route("api/badges")]
public class BadgesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBadgeImageGenerator _generator;

    public BadgesController(IMediator mediator, IBadgeImageGenerator generator)
    {
        _mediator = mediator;
        _generator = generator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBadgeClass(
        CreateBadgeClassCommand command, 
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBadgeBySlug), new { slug = command.Name.ToLower().Replace(" ", "-") }, new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GettAllBadges(
        [FromQuery] bool? active,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllBadgesQuery(active), cancellationToken);
        return Ok(result);
    }
    [HttpGet("slug/{slug}")]
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
    var badge = await _mediator.Send(new GetBadgeByIdQuery(id));

    if (badge == null)
        return NotFound();

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
    [HttpGet("generate")]
    public async Task<IActionResult> GenerateBadgeImage()
    {
        var imageUrl = await _generator.GenerateAsync("template1", new BadgeRenderData { BadgeName = "Badge de Teste" });
        return Ok(new { imageUrl });
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBadge(
    Guid id,
    UpdateBadgeClassRequest request,
    CancellationToken cancellationToken)
    {
    var command = new UpdateBadgeClassCommand(id)
    {
        Name = request.Name,
        Description = request.Description,
        ImageUrl = request.ImageUrl,
        CriteriaNarrative = request.CriteriaNarrative,
        Version = request.Version
    };

    var updated = await _mediator.Send(command, cancellationToken);

    if (!updated)
        return NotFound(new { message = "Badge not found." });

    return NoContent();
}
}

